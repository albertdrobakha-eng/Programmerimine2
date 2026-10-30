using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
namespace KooliProjekt.Application.Features.Portfolios
{
 public class DeletePortfolioCommand : IRequest<OperationResult>, ITransactional { public int Id { get; set; } }
}