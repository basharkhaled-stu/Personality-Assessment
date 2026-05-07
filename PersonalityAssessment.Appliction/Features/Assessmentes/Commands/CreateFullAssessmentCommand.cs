using MediatR;
using PersonalityAssessment.Application.Features.Assessmentes.DTO;

namespace PersonalityAssessment.Application.Features.Assessmentes.Commands
{
    public record CreateFullAssessmentCommand(CreateFullAssessmentDTO DTO) 
        : IRequest<ReadAssessmentDTO>;
}
