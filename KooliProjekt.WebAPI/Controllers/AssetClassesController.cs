using System.Threading.Tasks;
using KooliProjekt.Application.Features.AssetClasses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class AssetClassesController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public AssetClassesController(IMediator mediator) => _mediator = mediator;

        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var result = await _mediator.Send(new GetAssetClassQuery { Id = id });
            return Result(result);
        }

        [HttpGet("list")]
        public async Task<IActionResult> List(int page = 1, int pageSize = 10)
        {
            var result = await _mediator.Send(new ListAssetClassesQuery { Page = page, PageSize = pageSize });
            return Result(result);
        }
    }
}
