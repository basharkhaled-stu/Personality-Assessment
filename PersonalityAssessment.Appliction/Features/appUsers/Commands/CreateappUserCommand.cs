using MediatR;
using PersonalityAssessment.Application.Features.Options.DTO;

namespace PersonalityAssessment.Application.Features.appUsers.Commands
{
    public record class CreateappUserCommand(RegisterappUserDTO DTO)
        : IRequest<bool>;

}
