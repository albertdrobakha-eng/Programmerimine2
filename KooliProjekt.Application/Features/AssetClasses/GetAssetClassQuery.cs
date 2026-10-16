using KooliProjekt.Application.Data;
using MediatR;

namespace KooliProjekt.Application.Features.AssetClasses
{
    public class GetAssetClassQuery : IRequest<AssetClass>
    {
        public int Id { get; set; }
    }
}
