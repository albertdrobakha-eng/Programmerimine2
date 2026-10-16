using System.Collections.Generic;
using KooliProjekt.Application.Data;
using MediatR;

namespace KooliProjekt.Application.Features.Portfolios
{
    public class ListPortfoliosQuery : IRequest<IEnumerable<Portfolio>>
    {
    }
}
