using KooliProjekt.Application.Data;
using MediatR;

namespace KooliProjekt.Application.Features.Assets
{
    public class GetAssetQuery : IRequest<Asset>
    {
        public int Id { get; set; }
    }
}
