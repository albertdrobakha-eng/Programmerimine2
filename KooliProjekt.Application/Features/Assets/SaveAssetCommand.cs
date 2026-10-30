using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
namespace KooliProjekt.Application.Features.Assets
{
 public class SaveAssetCommand : IRequest<OperationResult>, ITransactional
 {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ticker { get; set; }
        public int PortfolioId { get; set; }
        public int AssetClassId { get; set; }
 }
}