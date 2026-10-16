using KooliProjekt.Application.Data;
using MediatR;

namespace KooliProjekt.Application.Features.Portfolios
{
    public class GetPortfolioQuery : IRequest<Portfolio>
    {
        public int Id { get; set; }
    }
}
