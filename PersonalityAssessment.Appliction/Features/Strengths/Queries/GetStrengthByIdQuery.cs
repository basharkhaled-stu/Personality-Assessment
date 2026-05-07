using MediatR;
using PersonalityAssessment.Application.Features.Strengths.DTO;

namespace PersonalityAssessment.Application.Features.Strengths.Queries
{
    public record class GetStrengthByIdQuery(int id) :
        IRequest<ReadStrengthDTO>;

}
