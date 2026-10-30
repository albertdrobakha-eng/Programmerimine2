using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace KooliProjekt.Application.Features.Portfolios
{
 public class DeletePortfolioCommandHandler : IRequestHandler<DeletePortfolioCommand, OperationResult>
 {
  private readonly ApplicationDbContext _dbContext;
  public DeletePortfolioCommandHandler(ApplicationDbContext dbContext) => _dbContext = dbContext;
  public async Task<OperationResult> Handle(DeletePortfolioCommand request, CancellationToken cancellationToken)
  {
   var result = new OperationResult();
   var item = await _dbContext.Portfolios.FindAsync(new object[] { request.Id }, cancellationToken);
   if (item == null) return result.AddPropertyError(nameof(request.Id), "Portfolio was not found.");
   if (await _dbContext.Assets.AnyAsync(x => x.PortfolioId == request.Id, cancellationToken)) return result.AddPropertyError(nameof(request.Id), "Portfolio still contains assets.");
   _dbContext.Portfolios.Remove(item);
   await _dbContext.SaveChangesAsync(cancellationToken);
   return result;
  }
 }
}