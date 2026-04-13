using MediatR;
using PersonalityAssessment.Application.Features.Strengths.DTO;

namespace PersonalityAssessment.Application.Features.Strengths.Commands
{
    public record class CreateStrengthCommand(CreateStrengthDTO DTO)
        : IRequest<ReadStrengthDTO>;

}
