using MediatR;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.Options.DTO;
namespace PersonalityAssessment.Application.Features.Options.Queries
{
    public record class GetAllOptionQuery(PagingParameters p) :
        IRequest<PagedResult<ReadOptionDTO>>;


}
