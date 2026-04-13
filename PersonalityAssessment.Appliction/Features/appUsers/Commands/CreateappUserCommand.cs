using MediatR;
using PersonalityAssessment.Application.Features.Options.DTO;

namespace PersonalityAssessment.Application.Features.Options.Commands
{
    public record class CreateappUserCommand(RegisterappUserDTO DTO)
        : IRequest<bool>;

}
