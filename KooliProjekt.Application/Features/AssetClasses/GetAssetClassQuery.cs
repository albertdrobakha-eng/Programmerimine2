using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.AssetClasses
{
    public class GetAssetClassQuery : IRequest<OperationResult<AssetClassDto>>
    {
        public int Id { get; set; }
    }
}
