using MediatR;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.AssessmentStatuses.DTO;

namespace PersonalityAssessment.Application.Features.AssessmentStatuses.Queries
{
    public record class GetAllAssessmentStatusQuery(PagingParameters p) :
        IRequest<PagedResult<ReadAssessmentStatusDTO>>;


}
