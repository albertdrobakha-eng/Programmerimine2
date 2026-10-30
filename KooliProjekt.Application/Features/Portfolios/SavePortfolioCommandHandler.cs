using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace KooliProjekt.Application.Features.Portfolios
{
 public class SavePortfolioCommandHandler : IRequestHandler<SavePortfolioCommand, OperationResult>
 {
  private readonly ApplicationDbContext _dbContext;
  public SavePortfolioCommandHandler(ApplicationDbContext dbContext) => _dbContext = dbContext;
  public async Task<OperationResult> Handle(SavePortfolioCommand request, CancellationToken cancellationToken)
  {
   var result = new OperationResult();
   Portfolio item;
   if (request.Id == 0) { item = new Portfolio(); _dbContext.Portfolios.Add(item); }
   else { item = await _dbContext.Portfolios.FindAsync(new object[] { request.Id }, cancellationToken); if (item == null) return result.AddPropertyError(nameof(request.Id), "Portfolio was not found."); }
   item.Name = request.Name;
   await _dbContext.SaveChangesAsync(cancellationToken);
   return result;
  }
 }
}