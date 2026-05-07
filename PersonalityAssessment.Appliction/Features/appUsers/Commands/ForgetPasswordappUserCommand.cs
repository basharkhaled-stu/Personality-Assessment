using MediatR;
using PersonalityAssessment.Application.Features.Options.DTO;

namespace PersonalityAssessment.Application.Features.appUsers.Commands
{
    public record ForgetPasswordappUserCommand(ForgetPasswordappUserDTO Dto)
        : IRequest<ForgetPasswordappUserResultDTO>;
}
