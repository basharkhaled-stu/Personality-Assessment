using MediatR;
using PersonalityAssessment.Application.Features.Assessmentes.DTO;

namespace PersonalityAssessment.Application.Features.Assessmentes.Queries
{
    public record class GetAssessmentByIdAdmainQuery(int id) :
        IRequest<AdmainReadAssessmentDTO>;

}
