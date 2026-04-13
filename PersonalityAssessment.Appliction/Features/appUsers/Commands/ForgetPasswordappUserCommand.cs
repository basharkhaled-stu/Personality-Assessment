using MediatR;
using PersonalityAssessment.Application.Features.Options.DTO;

namespace PersonalityAssessment.Application.Features.Options.Commands
{
    public record ForgetPasswordappUserCommand(ForgetPasswordappUserDTO Dto)
        : IRequest<ForgetPasswordappUserResultDTO>;
}
