using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace KooliProjekt.Application.Features.Assets
{
 public class SaveAssetCommandHandler : IRequestHandler<SaveAssetCommand, OperationResult>
 {
  private readonly ApplicationDbContext _dbContext;
  public SaveAssetCommandHandler(ApplicationDbContext dbContext) => _dbContext = dbContext;
  public async Task<OperationResult> Handle(SaveAssetCommand request, CancellationToken cancellationToken)
  {
   var result = new OperationResult();
   Asset item;
   if (request.Id == 0) { item = new Asset(); _dbContext.Assets.Add(item); }
   else { item = await _dbContext.Assets.FindAsync(new object[] { request.Id }, cancellationToken); if (item == null) return result.AddPropertyError(nameof(request.Id), "Asset was not found."); }
   if (!await _dbContext.Portfolios.AnyAsync(x => x.Id == request.PortfolioId, cancellationToken)) return result.AddPropertyError(nameof(request.PortfolioId), "Portfolio was not found.");
   if (!await _dbContext.AssetClasses.AnyAsync(x => x.Id == request.AssetClassId, cancellationToken)) return result.AddPropertyError(nameof(request.AssetClassId), "Asset class was not found.");
   item.Name = request.Name;
   item.Ticker = request.Ticker;
   item.PortfolioId = request.PortfolioId;
   item.AssetClassId = request.AssetClassId;
   await _dbContext.SaveChangesAsync(cancellationToken);
   return result;
  }
 }
}