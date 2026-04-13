using MediatR;
using PersonalityAssessment.Application.Features.Weaknesses.DTO;

namespace PersonalityAssessment.Application.Features.Weaknesses.Queries
{
    public record class GetWeakneesByIdAdmainQuery(int id) :
        IRequest<AdmainReadWeaknessDTO>;

}
