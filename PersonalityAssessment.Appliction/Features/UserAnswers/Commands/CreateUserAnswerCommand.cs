using MediatR;
using PersonalityAssessment.Application.Features.UserAnswers.DTO;

namespace PersonalityAssessment.Application.Features.UserAnswers.Commands
{
    public record class CreateUserAnswerCommand(CreateUserAnswerDTO DTO)
        : IRequest<ReadUserAnswerDTO>;

}
