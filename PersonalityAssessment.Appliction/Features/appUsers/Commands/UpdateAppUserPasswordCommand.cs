using MediatR;
using PersonalityAssessment.Application.Features.Options.DTO;

namespace PersonalityAssessment.Application.Features.Options.Commands
{
    public record UpdateAppUserPasswordCommand(string UserId, UpdatePasswordappUserDTO Dto)
        : IRequest<bool>;
}
