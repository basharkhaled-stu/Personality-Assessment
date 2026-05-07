using MediatR;
using PersonalityAssessment.Application.Features.Weaknesses.DTO;

namespace PersonalityAssessment.Application.Features.Weaknesses.Queries
{
    public record class GetWeakneesByIdQuery(int id) :
        IRequest<ReadWeakneesDTO>;

}
