using MediatR;
using PersonalityAssessment.Application.Features.Weaknesses.DTO;

namespace PersonalityAssessment.Application.Features.Weaknesses.Commands
{
    public record UpdateWeaknessCommand(int id, UpdateWeaknessDTO dto)
       : IRequest<bool>;
}
