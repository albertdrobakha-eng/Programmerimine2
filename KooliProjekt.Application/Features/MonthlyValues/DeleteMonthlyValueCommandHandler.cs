using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace KooliProjekt.Application.Features.MonthlyValues
{
 public class DeleteMonthlyValueCommandHandler : IRequestHandler<DeleteMonthlyValueCommand, OperationResult>
 {
  private readonly ApplicationDbContext _dbContext;
  public DeleteMonthlyValueCommandHandler(ApplicationDbContext dbContext) => _dbContext = dbContext;
  public async Task<OperationResult> Handle(DeleteMonthlyValueCommand request, CancellationToken cancellationToken)
  {
   var result = new OperationResult();
   var item = await _dbContext.MonthlyValues.FindAsync(new object[] { request.Id }, cancellationToken);
   if (item == null) return result.AddPropertyError(nameof(request.Id), "MonthlyValue was not found.");
   
   _dbContext.MonthlyValues.Remove(item);
   await _dbContext.SaveChangesAsync(cancellationToken);
   return result;
  }
 }
}