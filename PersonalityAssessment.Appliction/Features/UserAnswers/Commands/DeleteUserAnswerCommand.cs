using MediatR;

namespace PersonalityAssessment.Application.Features.UserAnswers.Commands
{
    public record DeleteUserAnswerCommand(int id) : IRequest<bool>;
}
