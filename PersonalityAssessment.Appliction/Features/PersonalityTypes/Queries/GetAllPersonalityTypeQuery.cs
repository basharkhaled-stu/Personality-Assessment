using MediatR;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.PersonalityTypes.DTO;
namespace PersonalityAssessment.Application.Features.PersonalityTypes.Queries
{
    public record class GetAllPersonalityTypeQuery(PagingParameters p) :
        IRequest<PagedResult<ReadPersonalityTypeDTO>>;


}
