using MediatR;
using PersonalityAssessment.Application.Features.Options.DTO;

namespace PersonalityAssessment.Application.Features.Options.Commands
{
    public record UpdateAppUserUsernameCommand(string UserId, UpdateUsernameappUserDTO Dto)
        : IRequest<bool>;
}
