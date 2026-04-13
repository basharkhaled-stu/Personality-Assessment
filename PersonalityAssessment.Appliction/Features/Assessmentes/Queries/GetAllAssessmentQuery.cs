using MediatR;
using PersonalityAssessment.Application.Common.DTOS;
using PersonalityAssessment.Application.Features.Assessmentes.DTO;
namespace PersonalityAssessment.Application.Features.Assessmentes.Queries
{
    public record class GetAllAssessmentQuery(PagingParameters p) :
        IRequest<PagedResult<ReadAssessmentDTO>>;


}
