using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Files;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Assets
{
    public class AddFileCommandHandler : IRequestHandler<AddFileCommand, OperationResult>
    {
        private readonly ApplicationDbContext _db;
        private readonly IFileClient _files;
        public AddFileCommandHandler(ApplicationDbContext db, IFileClient files) { _db = db; _files = files; }
        public async Task<OperationResult> Handle(AddFileCommand request, CancellationToken token)
        {
            if (!await _db.Assets.AnyAsync(x => x.Id == request.AssetId, token)) return new OperationResult().AddPropertyError(nameof(request.AssetId), "Asset was not found.");
            await _files.Save(request.FileName, FileStore.AssetPhotos(request.AssetId), request.InputStream);
            return new OperationResult();
        }
    }

    public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, OperationResult>
    {
        private readonly ApplicationDbContext _db;
        private readonly IFileClient _files;
        public DeleteFileCommandHandler(ApplicationDbContext db, IFileClient files) { _db = db; _files = files; }
        public async Task<OperationResult> Handle(DeleteFileCommand request, CancellationToken token)
        {
            if (!await _db.Assets.AnyAsync(x => x.Id == request.AssetId, token)) return new OperationResult().AddPropertyError(nameof(request.AssetId), "Asset was not found.");
            var existingFiles = await _files.List(FileStore.AssetPhotos(request.AssetId));
            if (!existingFiles.Contains(request.FileName)) return new OperationResult().AddPropertyError(nameof(request.FileName), "File was not found.");
            await _files.Delete(request.FileName, FileStore.AssetPhotos(request.AssetId));
            return new OperationResult();
        }
    }

    public class GetFileQueryHandler : IRequestHandler<GetFileQuery, OperationResult<AssetFileDto>>
    {
        private readonly ApplicationDbContext _db;
        private readonly IFileClient _files;
        public GetFileQueryHandler(ApplicationDbContext db, IFileClient files) { _db = db; _files = files; }
        public async Task<OperationResult<AssetFileDto>> Handle(GetFileQuery request, CancellationToken token)
        {
            if (!await _db.Assets.AnyAsync(x => x.Id == request.AssetId, token)) return new OperationResult<AssetFileDto>().AddPropertyError(nameof(request.AssetId), "Asset was not found.");
            using var stream = await _files.Get(request.FileName, FileStore.AssetPhotos(request.AssetId));
            if (stream == null) return new OperationResult<AssetFileDto>().AddError("File was not found.");
            using var memory = new MemoryStream();
            await stream.CopyToAsync(memory, token);
            return new OperationResult<AssetFileDto>(new AssetFileDto { FileName = request.FileName, Content = memory.ToArray() });
        }
    }

    public class ListFilesQueryHandler : IRequestHandler<ListFilesQuery, OperationResult<System.Collections.Generic.List<string>>>
    {
        private readonly ApplicationDbContext _db;
        private readonly IFileClient _files;
        public ListFilesQueryHandler(ApplicationDbContext db, IFileClient files) { _db = db; _files = files; }
        public async Task<OperationResult<System.Collections.Generic.List<string>>> Handle(ListFilesQuery request, CancellationToken token)
        {
            if (!await _db.Assets.AnyAsync(x => x.Id == request.AssetId, token)) return new OperationResult<System.Collections.Generic.List<string>>().AddPropertyError(nameof(request.AssetId), "Asset was not found.");
            var names = await _files.List(FileStore.AssetPhotos(request.AssetId));
            return new OperationResult<System.Collections.Generic.List<string>>(names.ToList());
        }
    }
}
