using System.Threading.Tasks;
using KooliProjekt.Application.Features.Portfolios;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class PortfoliosController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public PortfoliosController(IMediator mediator) => _mediator = mediator;

        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var result = await _mediator.Send(new GetPortfolioQuery { Id = id });
            if (result == null)
            {
                return NotFound();
            }

            return Json(result);
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var result = await _mediator.Send(new ListPortfoliosQuery());
            return Json(result);
        }
    }
}
