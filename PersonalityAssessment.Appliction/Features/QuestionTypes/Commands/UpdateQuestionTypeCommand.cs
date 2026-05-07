using MediatR;
using PersonalityAssessment.Application.Features.QuestionTypes.DTO;

namespace PersonalityAssessment.Application.Features.QuestionTypes.Commands
{
    public record UpdateQuestionTypeCommand(int id, UpdateQuestionTypeDTO dto)
       : IRequest<bool>;
}
