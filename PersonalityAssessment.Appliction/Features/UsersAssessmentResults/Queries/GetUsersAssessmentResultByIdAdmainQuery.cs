using MediatR;
using PersonalityAssessment.Application.Features.UsersAssessmentResults.DTO;

namespace PersonalityAssessment.Application.Features.UsersAssessmentResults.Queries
{
    public record class GetUsersAssessmentResultByIdAdmainQuery(int id) :
        IRequest<AdmainReadUsersAssessmentResultDTO>;

}
