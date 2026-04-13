using MediatR;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.UsersAssessmentResultPersonalityTypes.DTO;
namespace PersonalityAssessment.Application.Features.UsersAssessmentResultPersonalityTypes.Queries
{
    public record class GetAllUsersAssessmentResultPersonalityTypeQuery(PagingParameters p) :
        IRequest<PagedResult<ReadUsersAssessmentResultPersonalityTypeDTO>>;


}
