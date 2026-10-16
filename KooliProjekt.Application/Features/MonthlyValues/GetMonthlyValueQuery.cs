using KooliProjekt.Application.Data;
using MediatR;

namespace KooliProjekt.Application.Features.MonthlyValues
{
    public class GetMonthlyValueQuery : IRequest<MonthlyValue>
    {
        public int Id { get; set; }
    }
}
