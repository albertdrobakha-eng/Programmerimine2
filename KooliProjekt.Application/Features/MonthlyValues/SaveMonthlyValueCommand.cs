using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
namespace KooliProjekt.Application.Features.MonthlyValues
{
 public class SaveMonthlyValueCommand : IRequest<OperationResult>, ITransactional
 {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Quantity { get; set; }
        public decimal Value { get; set; }
        public decimal Total { get; set; }
 }
}