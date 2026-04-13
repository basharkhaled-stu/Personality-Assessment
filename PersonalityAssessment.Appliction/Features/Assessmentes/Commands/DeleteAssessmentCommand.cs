using MediatR;

namespace PersonalityAssessment.Application.Features.Assessmentes.Commands
{
    public record DeleteAssessmentCommand(int id) : IRequest<bool>;
}
