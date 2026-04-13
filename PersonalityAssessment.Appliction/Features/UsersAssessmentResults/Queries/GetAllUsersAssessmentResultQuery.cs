using MediatR;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.UsersAssessmentResults.DTO;

namespace PersonalityAssessment.Application.Features.UsersAssessmentResults.Queries
{
    public record class GetAllUsersAssessmentResultQuery(PagingParameters p) :
        IRequest<PagedResult<ReadUsersAssessmentResultDTO>>;


}
