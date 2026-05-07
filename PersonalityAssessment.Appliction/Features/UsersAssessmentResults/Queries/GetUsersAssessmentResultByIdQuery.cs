using MediatR;
using PersonalityAssessment.Application.Features.UsersAssessmentResults.DTO;

namespace PersonalityAssessment.Application.Features.UsersAssessmentResults.Queries
{
    public record class GetUsersAssessmentResultByIdQuery(int id) :
        IRequest<ReadUsersAssessmentResultDTO>;

}
