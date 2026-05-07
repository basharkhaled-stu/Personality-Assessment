using MediatR;
using PersonalityAssessment.Application.Features.Options.DTO;

namespace PersonalityAssessment.Application.Features.appUsers.Commands
{
    public record UpdateAppUserProfileCommand(string UserId, UpdateProfileappUserDTO Dto)
        : IRequest<bool>;
}
