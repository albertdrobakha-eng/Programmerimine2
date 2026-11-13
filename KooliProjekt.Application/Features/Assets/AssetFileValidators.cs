using FluentValidation;
using KooliProjekt.Application.Data;
using System.IO;
using System.Linq;

namespace KooliProjekt.Application.Features.Assets
{
    public class AddFileCommandValidator : AbstractValidator<AddFileCommand>
    {
        public AddFileCommandValidator(ApplicationDbContext db)
        {
            RuleFor(x => x.AssetId).GreaterThan(0).Must(id => db.Assets.Any(a => a.Id == id)).WithMessage("Asset was not found.");
            RuleFor(x => x.FileName).NotEmpty().Must(IsPlainFileName).WithMessage("A plain file name is required.").MaximumLength(255);
            RuleFor(x => x.InputStream).NotNull().WithMessage("File content is required.");
            RuleFor(x => x.InputStream).Must(stream => stream == null || !stream.CanSeek || stream.Length > 0).WithMessage("File content must not be empty.");
            RuleFor(x => x.InputStream).Must(stream => stream == null || !stream.CanSeek || stream.Length <= 20 * 1024 * 1024).WithMessage("File must not exceed 20 MB.");
        }
        internal static bool IsPlainFileName(string name) => !string.IsNullOrWhiteSpace(name) && Path.GetFileName(name) == name && !name.Contains('/') && !name.Contains('\\');
    }

    public class DeleteFileCommandValidator : AbstractValidator<DeleteFileCommand>
    {
        public DeleteFileCommandValidator(ApplicationDbContext db)
        {
            RuleFor(x => x.AssetId).GreaterThan(0).Must(id => db.Assets.Any(a => a.Id == id)).WithMessage("Asset was not found.");
            RuleFor(x => x.FileName).NotEmpty().Must(AddFileCommandValidator.IsPlainFileName).WithMessage("A plain file name is required.").MaximumLength(255);
        }
    }

    public class GetFileQueryValidator : AbstractValidator<GetFileQuery>
    {
        public GetFileQueryValidator(ApplicationDbContext db)
        {
            RuleFor(x => x.AssetId).GreaterThan(0).Must(id => db.Assets.Any(a => a.Id == id)).WithMessage("Asset was not found.");
            RuleFor(x => x.FileName).NotEmpty().Must(AddFileCommandValidator.IsPlainFileName).WithMessage("A plain file name is required.").MaximumLength(255);
        }
    }

    public class ListFilesQueryValidator : AbstractValidator<ListFilesQuery>
    {
        public ListFilesQueryValidator(ApplicationDbContext db)
        {
            RuleFor(x => x.AssetId).GreaterThan(0).Must(id => db.Assets.Any(a => a.Id == id)).WithMessage("Asset was not found.");
        }
    }
}
