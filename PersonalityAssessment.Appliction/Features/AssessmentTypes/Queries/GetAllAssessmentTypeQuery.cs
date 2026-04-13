using MediatR;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.AssessmentTypes.DTO;

namespace PersonalityAssessment.Application.Features.AssessmentTypes.Queries
{
    public record class GetAllAssessmentTypeQuery(PagingParameters p) :
        IRequest<PagedResult<ReadAssessmentTypeDTO>>;


}
