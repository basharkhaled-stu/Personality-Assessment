using MediatR;

namespace PersonalityAssessment.Application.Features.AssessmentTypes.Commands
{
    public record DeleteAssessmentTypeCommand(int id) : IRequest<bool>;
}
