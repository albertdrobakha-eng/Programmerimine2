using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using MediatR;

namespace KooliProjekt.Application.Features.Assets
{
    public class GetAssetQueryHandler : IRequestHandler<GetAssetQuery, Asset>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetAssetQueryHandler(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<Asset> Handle(GetAssetQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Assets.FindAsync(new object[] { request.Id }, cancellationToken);
        }
    }
}
