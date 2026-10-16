using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Portfolios
{
    public class ListPortfoliosQueryHandler : IRequestHandler<ListPortfoliosQuery, IEnumerable<Portfolio>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListPortfoliosQueryHandler(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<Portfolio>> Handle(ListPortfoliosQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Portfolios.ToListAsync(cancellationToken);
        }
    }
}
