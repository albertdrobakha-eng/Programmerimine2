using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace KooliProjekt.Application.Features.Assets
{
 public class DeleteAssetCommandHandler : IRequestHandler<DeleteAssetCommand, OperationResult>
 {
  private readonly ApplicationDbContext _dbContext;
  public DeleteAssetCommandHandler(ApplicationDbContext dbContext) => _dbContext = dbContext;
  public async Task<OperationResult> Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
  {
   var result = new OperationResult();
   var item = await _dbContext.Assets.FindAsync(new object[] { request.Id }, cancellationToken);
   if (item == null) return result.AddPropertyError(nameof(request.Id), "Asset was not found.");
   await _dbContext.MonthlyValues.Where(x => x.AssetId == request.Id).ExecuteDeleteAsync(cancellationToken);
   _dbContext.Assets.Remove(item);
   await _dbContext.SaveChangesAsync(cancellationToken);
   return result;
  }
 }
}