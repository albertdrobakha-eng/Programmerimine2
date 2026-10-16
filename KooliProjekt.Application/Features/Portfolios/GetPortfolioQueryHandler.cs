using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;

namespace KooliProjekt.Application.Features.Portfolios
{
    public class GetPortfolioQueryHandler : IRequestHandler<GetPortfolioQuery, Portfolio>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetPortfolioQueryHandler(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<Portfolio> Handle(GetPortfolioQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Portfolios.FindAsync(new object[] { request.Id }, cancellationToken);
        }
    }
}
