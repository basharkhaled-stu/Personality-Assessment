using MediatR;

namespace PersonalityAssessment.Application.Features.UsersAssessmentResults.Commands
{
    public record DeleteUsersAssessmentResultCommand(int id) : IRequest<bool>;
}
