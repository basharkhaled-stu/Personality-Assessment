using MediatR;

namespace PersonalityAssessment.Application.Features.Questions.Commands
{
    public record DeleteQuestionCommand(int id) : IRequest<bool>;
}
