using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.MonthlyValues
{
    public class GetMonthlyValueQueryHandler : IRequestHandler<GetMonthlyValueQuery, OperationResult<MonthlyValueDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetMonthlyValueQueryHandler(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<OperationResult<MonthlyValueDto>> Handle(GetMonthlyValueQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<MonthlyValueDto>();
            result.Value = await _dbContext.MonthlyValues.AsNoTracking().Where(x => x.Id == request.Id)
                .Select(x => new MonthlyValueDto { Id = x.Id, AssetId = x.AssetId, AssetName = x.Asset.Name, Ticker = x.Asset.Ticker, Year = x.Year, Month = x.Month, Quantity = x.Quantity, Value = x.Value, Total = x.Total })
                .FirstOrDefaultAsync(cancellationToken);
            return result;
        }
    }
}
