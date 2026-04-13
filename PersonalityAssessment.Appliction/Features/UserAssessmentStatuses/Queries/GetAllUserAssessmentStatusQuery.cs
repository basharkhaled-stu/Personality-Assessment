using MediatR;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.UserAssessmentStatuses.DTO;
namespace PersonalityAssessment.Application.Features.UserAssessmentStatuses.Queries
{
    public record class GetAllUserAssessmentStatusQuery(PagingParameters p) :
        IRequest<PagedResult<ReadUserAssessmentStatusDTO>>;


}
