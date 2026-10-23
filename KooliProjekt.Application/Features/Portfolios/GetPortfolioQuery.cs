using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Portfolios
{
    public class GetPortfolioQuery : IRequest<OperationResult<PortfolioDto>>
    {
        public int Id { get; set; }
    }
}
