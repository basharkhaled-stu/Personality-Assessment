using MediatR;
using PersonalityAssessment.Application.Features.QuestionTypes.DTO;

namespace PersonalityAssessment.Application.Features.QuestionTypes.Commands
{
    public record class CreateQuestionTypeCommand(CreateQuestionTypeDTO DTO)
        : IRequest<ReadQuestionTypeDTO>;

}
