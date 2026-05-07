using MediatR;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.Weaknesses.DTO;

namespace PersonalityAssessment.Application.Features.Weaknesses.Queries
{
    public record class GetAllWeakneesQuery(PagingParameters p) :
        IRequest<PagedResult<ReadWeakneesDTO>>;


}
