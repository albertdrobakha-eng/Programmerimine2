using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using System.Linq;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Assets
{
    public class GetAssetQueryHandler : IRequestHandler<GetAssetQuery, OperationResult<AssetDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetAssetQueryHandler(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<OperationResult<AssetDto>> Handle(GetAssetQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<AssetDto>();
            result.Value = await _dbContext.Assets.AsNoTracking().Where(x => x.Id == request.Id)
                .Select(x => new AssetDto { Id = x.Id, Name = x.Name, Ticker = x.Ticker, PortfolioId = x.PortfolioId, PortfolioName = x.Portfolio.Name, AssetClassId = x.AssetClassId, AssetClassName = x.AssetClass.Name })
                .FirstOrDefaultAsync(cancellationToken);
            return result;
        }
    }
}
