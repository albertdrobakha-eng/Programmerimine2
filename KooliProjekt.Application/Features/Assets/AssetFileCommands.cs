using System.IO;
using System.Collections.Generic;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Assets
{
    public class AddFileCommand : IRequest<OperationResult>
    {
        public int AssetId { get; set; }
        public string FileName { get; set; }
        public Stream InputStream { get; set; }
    }

    public class DeleteFileCommand : IRequest<OperationResult>
    {
        public int AssetId { get; set; }
        public string FileName { get; set; }
    }

    public class GetFileQuery : IRequest<OperationResult<AssetFileDto>>
    {
        public int AssetId { get; set; }
        public string FileName { get; set; }
    }

    public class ListFilesQuery : IRequest<OperationResult<List<string>>>
    {
        public int AssetId { get; set; }
    }

    public class AssetFileDto
    {
        public string FileName { get; set; }
        public byte[] Content { get; set; }
    }
}
