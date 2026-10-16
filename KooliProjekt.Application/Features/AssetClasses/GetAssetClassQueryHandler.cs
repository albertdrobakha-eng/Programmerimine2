using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;

namespace KooliProjekt.Application.Features.AssetClasses
{
    public class GetAssetClassQueryHandler : IRequestHandler<GetAssetClassQuery, AssetClass>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetAssetClassQueryHandler(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<AssetClass> Handle(GetAssetClassQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.AssetClasses.FindAsync(new object[] { request.Id }, cancellationToken);
        }
    }
}
