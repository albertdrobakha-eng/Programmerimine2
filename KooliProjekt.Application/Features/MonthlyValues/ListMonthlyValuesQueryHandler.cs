using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.MonthlyValues
{
    public class ListMonthlyValuesQueryHandler : IRequestHandler<ListMonthlyValuesQuery, IEnumerable<MonthlyValue>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListMonthlyValuesQueryHandler(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<MonthlyValue>> Handle(ListMonthlyValuesQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.MonthlyValues.ToListAsync(cancellationToken);
        }
    }
}
