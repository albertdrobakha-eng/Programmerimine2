using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.MonthlyValues
{
    public class GetMonthlyValueQuery : IRequest<OperationResult<MonthlyValueDto>>
    {
        public int Id { get; set; }
    }
}
