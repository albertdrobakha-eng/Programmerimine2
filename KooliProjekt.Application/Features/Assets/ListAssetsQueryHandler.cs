using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Assets
{
    public class ListAssetsQueryHandler : IRequestHandler<ListAssetsQuery, OperationResult<PagedResult<AssetDto>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListAssetsQueryHandler(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<OperationResult<PagedResult<AssetDto>>> Handle(ListAssetsQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<AssetDto>>();
            result.Value = await _dbContext.Assets.AsNoTracking().OrderBy(x => x.Name)
                .Select(x => new AssetDto { Id = x.Id, Name = x.Name, Ticker = x.Ticker, PortfolioId = x.PortfolioId, PortfolioName = x.Portfolio.Name, AssetClassId = x.AssetClassId, AssetClassName = x.AssetClass.Name })
                .GetPagedAsync(request.Page, request.PageSize);
            return result;
        }
    }
}
