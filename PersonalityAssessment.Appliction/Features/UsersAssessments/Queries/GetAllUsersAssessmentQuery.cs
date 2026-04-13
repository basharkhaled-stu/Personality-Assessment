using MediatR;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.UsersAssessments.DTO;
namespace PersonalityAssessment.Application.Features.UsersAssessments.Queries
{
    public record class GetAllUsersAssessmentQuery(PagingParameters p) :
        IRequest<PagedResult<ReadUsersAssessmentDTO>>;


}
