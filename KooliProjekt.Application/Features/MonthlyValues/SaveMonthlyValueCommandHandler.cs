using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace KooliProjekt.Application.Features.MonthlyValues
{
 public class SaveMonthlyValueCommandHandler : IRequestHandler<SaveMonthlyValueCommand, OperationResult>
 {
  private readonly ApplicationDbContext _dbContext;
  public SaveMonthlyValueCommandHandler(ApplicationDbContext dbContext) => _dbContext = dbContext;
  public async Task<OperationResult> Handle(SaveMonthlyValueCommand request, CancellationToken cancellationToken)
  {
   var result = new OperationResult();
   MonthlyValue item;
   if (request.Id == 0) { item = new MonthlyValue(); _dbContext.MonthlyValues.Add(item); }
   else { item = await _dbContext.MonthlyValues.FindAsync(new object[] { request.Id }, cancellationToken); if (item == null) return result.AddPropertyError(nameof(request.Id), "MonthlyValue was not found."); }
   if (!await _dbContext.Assets.AnyAsync(x => x.Id == request.AssetId, cancellationToken)) return result.AddPropertyError(nameof(request.AssetId), "Asset was not found.");
   item.AssetId = request.AssetId;
   item.Year = request.Year;
   item.Month = request.Month;
   item.Quantity = request.Quantity;
   item.Value = request.Value;
   item.Total = request.Total;
   await _dbContext.SaveChangesAsync(cancellationToken);
   return result;
  }
 }
}