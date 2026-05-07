using MediatR;
using PersonalityAssessment.Application.Features.Weaknesses.DTO;

namespace PersonalityAssessment.Application.Features.Weaknesses.Commands
{
    public record class CreateWeaknessCommand(CreateWeaknessDTO DTO)
        : IRequest<ReadWeakneesDTO>;

}
