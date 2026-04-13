using MediatR;
using PersonalityAssessment.Application.Features.Assessmentes.DTO;

namespace PersonalityAssessment.Application.Features.Assessmentes.Commands
{
    public record UpdateAssessmentCommand(int id, UpdateAssessmentDTO dto)
       : IRequest<bool>;
}
