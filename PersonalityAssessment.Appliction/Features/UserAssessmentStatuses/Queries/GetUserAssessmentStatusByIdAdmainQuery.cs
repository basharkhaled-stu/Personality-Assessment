using MediatR;
using PersonalityAssessment.Application.Features.UserAssessmentStatuses.DTO;

namespace PersonalityAssessment.Application.Features.UserAssessmentStatuses.Queries
{
    public record class GetUserAssessmentStatusByIdAdmainQuery(int id) :
        IRequest<AdmainUserAssessmentStatusDTO>;

}
