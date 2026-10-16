using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;

namespace KooliProjekt.Application.Features.MonthlyValues
{
    public class GetMonthlyValueQueryHandler : IRequestHandler<GetMonthlyValueQuery, MonthlyValue>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetMonthlyValueQueryHandler(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<MonthlyValue> Handle(GetMonthlyValueQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.MonthlyValues.FindAsync(new object[] { request.Id }, cancellationToken);
        }
    }
}
