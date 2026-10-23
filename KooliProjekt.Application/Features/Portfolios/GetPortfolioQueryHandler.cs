using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Portfolios
{
    public class GetPortfolioQueryHandler : IRequestHandler<GetPortfolioQuery, OperationResult<PortfolioDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetPortfolioQueryHandler(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<OperationResult<PortfolioDto>> Handle(GetPortfolioQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PortfolioDto>();
            result.Value = await _dbContext.Portfolios.AsNoTracking().Where(x => x.Id == request.Id)
                .Select(x => new PortfolioDto { Id = x.Id, Name = x.Name, AssetCount = x.Assets.Count })
                .FirstOrDefaultAsync(cancellationToken);
            return result;
        }
    }
}
