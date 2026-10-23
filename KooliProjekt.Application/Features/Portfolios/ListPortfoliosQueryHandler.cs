using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Portfolios
{
    public class ListPortfoliosQueryHandler : IRequestHandler<ListPortfoliosQuery, OperationResult<PagedResult<PortfolioDto>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListPortfoliosQueryHandler(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<OperationResult<PagedResult<PortfolioDto>>> Handle(ListPortfoliosQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<PortfolioDto>>();
            result.Value = await _dbContext.Portfolios.AsNoTracking().OrderBy(x => x.Name)
                .Select(x => new PortfolioDto { Id = x.Id, Name = x.Name, AssetCount = x.Assets.Count })
                .GetPagedAsync(request.Page, request.PageSize);
            return result;
        }
    }
}
