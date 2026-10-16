using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.AssetClasses
{
    public class ListAssetClassesQueryHandler : IRequestHandler<ListAssetClassesQuery, IEnumerable<AssetClass>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListAssetClassesQueryHandler(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<AssetClass>> Handle(ListAssetClassesQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.AssetClasses.ToListAsync(cancellationToken);
        }
    }
}
