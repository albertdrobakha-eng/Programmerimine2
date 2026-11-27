using System.Threading.Tasks;
using KooliProjekt.Application.Features.Portfolios;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class PortfoliosController : AdminApiControllerBase
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
            return Result(result);
        }

        [HttpGet("list")]
        public async Task<IActionResult> List(int page = 1, int pageSize = 10)
        {
            var result = await _mediator.Send(new ListPortfoliosQuery { Page = page, PageSize = pageSize });
            return Result(result);
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] SavePortfolioCommand command) => Result(await _mediator.Send(command));

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DeletePortfolioCommand command) => Result(await _mediator.Send(command));
    }
}
