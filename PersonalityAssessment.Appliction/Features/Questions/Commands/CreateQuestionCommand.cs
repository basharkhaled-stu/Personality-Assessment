using MediatR;
using PersonalityAssessment.Application.Features.Questions.DTO;

namespace PersonalityAssessment.Application.Features.Questions.Commands
{
    public record class CreateQuestionCommand(CreateQuestionDTO DTO)
        : IRequest<ReadQuestionDTO>;

}
