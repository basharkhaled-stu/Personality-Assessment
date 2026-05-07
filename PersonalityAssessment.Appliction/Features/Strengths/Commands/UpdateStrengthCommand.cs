using MediatR;
using PersonalityAssessment.Application.Features.Strengths.DTO;

namespace PersonalityAssessment.Application.Features.Strengths.Commands
{
    public record UpdateStrengthCommand(int id, UpdateStrengthDTO dto)
       : IRequest<bool>;
}
