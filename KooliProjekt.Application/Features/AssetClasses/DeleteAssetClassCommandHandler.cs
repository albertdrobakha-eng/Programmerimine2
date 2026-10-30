using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace KooliProjekt.Application.Features.AssetClasses
{
 public class DeleteAssetClassCommandHandler : IRequestHandler<DeleteAssetClassCommand, OperationResult>
 {
  private readonly ApplicationDbContext _dbContext;
  public DeleteAssetClassCommandHandler(ApplicationDbContext dbContext) => _dbContext = dbContext;
  public async Task<OperationResult> Handle(DeleteAssetClassCommand request, CancellationToken cancellationToken)
  {
   var result = new OperationResult();
   var item = await _dbContext.AssetClasses.FindAsync(new object[] { request.Id }, cancellationToken);
   if (item == null) return result.AddPropertyError(nameof(request.Id), "AssetClass was not found.");
   if (await _dbContext.Assets.AnyAsync(x => x.AssetClassId == request.Id, cancellationToken)) return result.AddPropertyError(nameof(request.Id), "Asset class is assigned to assets.");
   _dbContext.AssetClasses.Remove(item);
   await _dbContext.SaveChangesAsync(cancellationToken);
   return result;
  }
 }
}