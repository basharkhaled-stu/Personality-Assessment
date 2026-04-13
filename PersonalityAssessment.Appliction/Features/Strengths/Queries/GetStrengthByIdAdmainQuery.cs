using MediatR;
using PersonalityAssessment.Application.Features.Strengths.DTO;

namespace PersonalityAssessment.Application.Features.Strengths.Queries
{
    public record class GetStrengthByIdAdmainQuery(int id) :
        IRequest<AdmainReadStrengthDTO>;

}
