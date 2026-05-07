using MediatR;
using PersonalityAssessment.Application.Features.Assessmentes.DTO;

namespace PersonalityAssessment.Application.Features.Assessments.Queries
{
    public record class GetAssessmentByIdQuery(int id) :
        IRequest<ReadAssessmentDTO>;

}
