using MediatR;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.QuestionTypes.DTO;

namespace PersonalityAssessment.Application.Features.QuestionTypes.Queries
{
    public record class GetAllQuestionTypeQuery(PagingParameters p) :
        IRequest<PagedResult<ReadQuestionTypeDTO>>;


}
