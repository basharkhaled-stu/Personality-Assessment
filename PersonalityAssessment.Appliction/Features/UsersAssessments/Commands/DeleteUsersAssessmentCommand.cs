using MediatR;

namespace PersonalityAssessment.Application.Features.UsersAssessments.Commands
{
    public record DeleteUsersAssessmentCommand(int id) : IRequest<bool>;
}
