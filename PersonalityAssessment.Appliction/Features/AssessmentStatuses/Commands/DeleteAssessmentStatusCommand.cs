using MediatR;

namespace PersonalityAssessment.Application.Features.AssessmentStatuses.Commands
{
    public record DeleteAssessmentStatusCommand(int id) : IRequest<bool>;
}
