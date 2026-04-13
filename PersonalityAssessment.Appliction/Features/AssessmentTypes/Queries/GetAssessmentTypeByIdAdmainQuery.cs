using MediatR;
using PersonalityAssessment.Application.Features.AssessmentTypes.DTO;

namespace PersonalityAssessment.Application.Features.AssessmentTypes.Queries
{
    public record class GetAssessmentTypeByIdAdmainQuery(int id) :
        IRequest<AdmainReadAssessmentTypeDTO>;

}
