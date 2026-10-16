using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Assets
{
    public class ListAssetsQueryHandler : IRequestHandler<ListAssetsQuery, IEnumerable<Asset>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListAssetsQueryHandler(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<Asset>> Handle(ListAssetsQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Assets.ToListAsync(cancellationToken);
        }
    }
}
