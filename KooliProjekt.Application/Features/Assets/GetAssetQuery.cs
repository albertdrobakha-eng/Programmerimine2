using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Assets
{
    public class GetAssetQuery : IRequest<OperationResult<AssetDto>>
    {
        public int Id { get; set; }
    }
}
