using MediatR;
using PersonalityAssessment.Application.Features.Options.DTO;

namespace PersonalityAssessment.Application.Features.appUsers.Commands
{
    public record UpdateAppUserPasswordCommand(string UserId, UpdatePasswordappUserDTO Dto)
        : IRequest<bool>;
}
