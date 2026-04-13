using MediatR;
using PersonalityAssessment.Application.Features.Questions.DTO;

namespace PersonalityAssessment.Application.Features.Questions.Commands
{
    public record UpdateQuestionCommand(int id, UpdateQuestionDTO dto)
       : IRequest<bool>;
}
