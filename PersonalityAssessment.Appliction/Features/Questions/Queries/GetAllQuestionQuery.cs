using MediatR;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.Questions.DTO;
namespace PersonalityAssessment.Application.Features.Questions.Queries
{
    public record class GetAllQuestionQuery(PagingParameters p) :
        IRequest<PagedResult<ReadQuestionDTO>>;


}
