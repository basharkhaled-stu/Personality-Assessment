using MediatR;
using PersonalityAssessment.Application.Features.Options.DTO;

namespace PersonalityAssessment.Application.Features.Options.Commands
{
    public record UpdateAppUserEmailCommand(string UserId, UpdateEmailappUserDTO Dto)
        : IRequest<bool>;
}
