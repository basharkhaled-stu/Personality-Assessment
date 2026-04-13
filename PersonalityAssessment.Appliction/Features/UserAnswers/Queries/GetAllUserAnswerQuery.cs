using MediatR;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.UserAnswers.DTO;
namespace PersonalityAssessment.Application.Features.UserAnswers.Queries
{
    public record class GetAllUserAnswerQuery(PagingParameters p) :
        IRequest<PagedResult<ReadUserAnswerDTO>>;


}
