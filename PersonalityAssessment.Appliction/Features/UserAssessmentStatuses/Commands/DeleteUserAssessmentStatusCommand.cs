using MediatR;

namespace PersonalityAssessment.Application.Features.UserAssessmentStatuses.Commands
{
    public record DeleteUserAssessmentStatusCommand(int id) : IRequest<bool>;
}
