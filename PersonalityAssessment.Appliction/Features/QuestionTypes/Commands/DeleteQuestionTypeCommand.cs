using MediatR;

namespace PersonalityAssessment.Application.Features.QuestionTypes.Commands
{
    public record DeleteQuestionTypeCommand(int id) : IRequest<bool>;
}
