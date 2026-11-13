using System.Threading.Tasks;
using KooliProjekt.Application.Features.Assets;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace KooliProjekt.WebAPI.Controllers
{
    public class AssetsController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public AssetsController(IMediator mediator) => _mediator = mediator;

        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var result = await _mediator.Send(new GetAssetQuery { Id = id });
            return Result(result);
        }

        [HttpGet("list")]
        public async Task<IActionResult> List(int page = 1, int pageSize = 10)
        {
            var result = await _mediator.Send(new ListAssetsQuery { Page = page, PageSize = pageSize });
            return Result(result);
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] SaveAssetCommand command) => Result(await _mediator.Send(command));

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteAssetCommand command) => Result(await _mediator.Send(command));

        [HttpPost("{assetId}/files")]
        [RequestSizeLimit(20971520)]
        public async Task<IActionResult> AddFile(int assetId, IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest("File is required.");
            await using var stream = file.OpenReadStream();
            return Result(await _mediator.Send(new AddFileCommand { AssetId = assetId, FileName = Path.GetFileName(file.FileName), InputStream = stream }));
        }

        [HttpDelete("{assetId}/files/{fileName}")]
        public async Task<IActionResult> DeleteFile(int assetId, string fileName) => Result(await _mediator.Send(new DeleteFileCommand { AssetId = assetId, FileName = fileName }));

        [HttpGet("{assetId}/files")]
        public async Task<IActionResult> ListFiles(int assetId) => Result(await _mediator.Send(new ListFilesQuery { AssetId = assetId }));

        [HttpGet("{assetId}/files/{fileName}")]
        public async Task<IActionResult> GetFile(int assetId, string fileName)
        {
            var result = await _mediator.Send(new GetFileQuery { AssetId = assetId, FileName = fileName });
            if (result.Errors != null && result.Errors.Contains("File was not found.")) return NotFound();
            if (result.HasErrors) return BadRequest(result);
            if (result.Value == null) return NotFound();
            return File(result.Value.Content, "application/octet-stream", result.Value.FileName);
        }
    }
}
