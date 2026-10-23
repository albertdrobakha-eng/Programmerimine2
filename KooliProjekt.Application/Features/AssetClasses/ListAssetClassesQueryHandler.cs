using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.AssetClasses
{
    public class ListAssetClassesQueryHandler : IRequestHandler<ListAssetClassesQuery, OperationResult<PagedResult<AssetClassDto>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListAssetClassesQueryHandler(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<OperationResult<PagedResult<AssetClassDto>>> Handle(ListAssetClassesQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<AssetClassDto>>();
            result.Value = await _dbContext.AssetClasses.AsNoTracking().OrderBy(x => x.Name)
                .Select(x => new AssetClassDto { Id = x.Id, Name = x.Name, AssetCount = x.Assets.Count })
                .GetPagedAsync(request.Page, request.PageSize);
            return result;
        }
    }
}
