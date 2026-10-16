using System.Collections.Generic;
using KooliProjekt.Application.Data;
using MediatR;

namespace KooliProjekt.Application.Features.MonthlyValues
{
    public class ListMonthlyValuesQuery : IRequest<IEnumerable<MonthlyValue>>
    {
    }
}
