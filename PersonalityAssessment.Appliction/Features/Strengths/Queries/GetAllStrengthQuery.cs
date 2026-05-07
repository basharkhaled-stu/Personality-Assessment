using MediatR;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.Strengths.DTO;

namespace PersonalityAssessment.Application.Features.Strengths.Queries
{
    public record class GetAllStrengthQuery(PagingParameters p) :
        IRequest<PagedResult<ReadStrengthDTO>>;


}
