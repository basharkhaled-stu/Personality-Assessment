using MediatR;
using PersonalityAssessment.Application.Features.Options.DTO;

namespace PersonalityAssessment.Application.Features.Options.Commands
{
    public record UpdateOptionCommand(int id, UpdateOptionDTO dto)
       : IRequest<bool>;
}
