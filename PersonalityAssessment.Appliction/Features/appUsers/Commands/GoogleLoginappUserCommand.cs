using MediatR;
using PersonalityAssessment.Application.Features.Options.DTO;

namespace PersonalityAssessment.Application.Features.Options.Commands
{
    public record GoogleLoginappUserCommand(GoogleLoginappUserDTO Dto)
        : IRequest<string?>;
}
