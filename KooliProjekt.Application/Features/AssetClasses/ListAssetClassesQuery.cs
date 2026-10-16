using System.Collections.Generic;
using KooliProjekt.Application.Data;
using MediatR;

namespace KooliProjekt.Application.Features.AssetClasses
{
    public class ListAssetClassesQuery : IRequest<IEnumerable<AssetClass>>
    {
    }
}
