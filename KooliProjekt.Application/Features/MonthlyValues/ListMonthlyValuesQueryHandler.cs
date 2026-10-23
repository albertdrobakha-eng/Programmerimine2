using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.MonthlyValues
{
    public class ListMonthlyValuesQueryHandler : IRequestHandler<ListMonthlyValuesQuery, OperationResult<PagedResult<MonthlyValueDto>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListMonthlyValuesQueryHandler(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<OperationResult<PagedResult<MonthlyValueDto>>> Handle(ListMonthlyValuesQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<MonthlyValueDto>>();
            result.Value = await _dbContext.MonthlyValues.AsNoTracking().OrderByDescending(x => x.Year).ThenByDescending(x => x.Month)
                .Select(x => new MonthlyValueDto { Id = x.Id, AssetId = x.AssetId, AssetName = x.Asset.Name, Ticker = x.Asset.Ticker, Year = x.Year, Month = x.Month, Quantity = x.Quantity, Value = x.Value, Total = x.Total })
                .GetPagedAsync(request.Page, request.PageSize);
            return result;
        }
    }
}
