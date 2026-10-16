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
            if (result == null)
            {
                return NotFound();
            }

            return Json(result);
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var result = await _mediator.Send(new ListAssetClassesQuery());
            return Json(result);
        }
    }
}
