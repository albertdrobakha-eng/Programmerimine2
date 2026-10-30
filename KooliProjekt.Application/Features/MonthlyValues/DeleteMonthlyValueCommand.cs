using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
namespace KooliProjekt.Application.Features.MonthlyValues
{
 public class DeleteMonthlyValueCommand : IRequest<OperationResult>, ITransactional { public int Id { get; set; } }
}