using System.Collections.Generic;
using KooliProjekt.Application.Data;
using MediatR;

namespace KooliProjekt.Application.Features.Assets
{
    public class ListAssetsQuery : IRequest<IEnumerable<Asset>>
    {
    }
}
