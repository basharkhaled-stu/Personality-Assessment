using MediatR;
using PersonalityAssessment.Application.Features.Options.DTO;

namespace PersonalityAssessment.Application.Features.Options.Commands
{
    public record UpdateAppUserLastNameCommand(string UserId, UpdateLastNameappUserDTO Dto)
        : IRequest<bool>;
}
