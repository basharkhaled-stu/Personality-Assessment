using MediatR;
using PersonalityAssessment.Application.Features.Options.DTO;

namespace PersonalityAssessment.Application.Features.appUsers.Commands
{
    public record UpdateAppUserEmailCommand(string UserId, UpdateEmailappUserDTO Dto)
        : IRequest<bool>;
}
