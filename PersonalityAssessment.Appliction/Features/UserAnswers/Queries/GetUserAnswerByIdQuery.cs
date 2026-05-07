using MediatR;
using PersonalityAssessment.Application.Features.UserAnswers.DTO;

namespace PersonalityAssessment.Application.Features.UserAnswers.Queries
{
    public record class GetUserAnswerByIdQuery(int id) :
        IRequest<ReadUserAnswerDTO>;

}
