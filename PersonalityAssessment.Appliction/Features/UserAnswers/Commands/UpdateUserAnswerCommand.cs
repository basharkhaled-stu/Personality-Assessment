using MediatR;
using PersonalityAssessment.Application.Features.UserAnswers.DTO;

namespace PersonalityAssessment.Application.Features.UserAnswers.Commands
{
    public record UpdateUserAnswerCommand(int id, UpdateUserAnswerDTO dto)
       : IRequest<bool>;
}
