using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
namespace KooliProjekt.Application.Features.Portfolios
{
 public class SavePortfolioCommand : IRequest<OperationResult>, ITransactional
 {
        public int Id { get; set; }
        public string Name { get; set; }
 }
}