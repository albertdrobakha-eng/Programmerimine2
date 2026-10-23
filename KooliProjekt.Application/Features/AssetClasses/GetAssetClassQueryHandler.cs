using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.AssetClasses
{
    public class GetAssetClassQueryHandler : IRequestHandler<GetAssetClassQuery, OperationResult<AssetClassDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetAssetClassQueryHandler(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<OperationResult<AssetClassDto>> Handle(GetAssetClassQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<AssetClassDto>();
            result.Value = await _dbContext.AssetClasses.AsNoTracking().Where(x => x.Id == request.Id)
                .Select(x => new AssetClassDto { Id = x.Id, Name = x.Name, AssetCount = x.Assets.Count })
                .FirstOrDefaultAsync(cancellationToken);
            return result;
        }
    }
}
