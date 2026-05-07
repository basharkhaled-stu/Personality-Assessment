using MediatR;
using PersonalityAssessment.Application.Features.Options.DTO;

namespace PersonalityAssessment.Application.Features.appUsers.Commands
{
    public record UpdateAppUserLastNameCommand(string UserId, UpdateLastNameappUserDTO Dto)
        : IRequest<bool>;
}
