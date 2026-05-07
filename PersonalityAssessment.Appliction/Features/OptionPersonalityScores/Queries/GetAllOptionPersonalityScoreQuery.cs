using MediatR;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.OptionPersonalityScores.DTO;
namespace PersonalityAssessment.Application.Features.OptionPersonalityScores.Queries
{
    public record class GetAllOptionPersonalityScoreQuery(PagingParameters p) :
        IRequest<PagedResult<ReadOptionPersonalityScoreDTO>>;


}
