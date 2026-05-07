using MediatR;
using PersonalityAssessment.Application.Features.Options.DTO;

namespace PersonalityAssessment.Application.Features.appUsers.Commands
{
    public record UpdateAppUserFirstNameCommand(string UserId, UpdateFirstNameappUserDTO Dto)
        : IRequest<bool>;
}
