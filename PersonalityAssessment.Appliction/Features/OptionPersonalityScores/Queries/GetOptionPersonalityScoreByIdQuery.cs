using MediatR;
using PersonalityAssessment.Application.Features.OptionPersonalityScores.DTO;

namespace PersonalityAssessment.Application.Features.OptionPersonalityScores.Queries
{
    public record class GetOptionPersonalityScoreByIdQuery(int id) :
        IRequest<ReadOptionPersonalityScoreDTO>;

}
