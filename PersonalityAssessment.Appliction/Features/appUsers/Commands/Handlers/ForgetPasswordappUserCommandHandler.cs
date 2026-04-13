using MediatR;
using PersonalityAssessment.Application.Features.Options.Commands;
using PersonalityAssessment.Application.Features.Options.DTO;
using PersonalityAssessment.Core.Interface;

namespace PersonalityAssessment.Application.Features.Options.Commands.Handlers
{
    public class ForgetPasswordappUserCommandHandler
        : IRequestHandler<ForgetPasswordappUserCommand, ForgetPasswordappUserResultDTO>
    {
        private readonly IIdentityUser _identityUser;

        public ForgetPasswordappUserCommandHandler(IIdentityUser identityUser)
        {
            _identityUser = identityUser;
        }

        public async Task<ForgetPasswordappUserResultDTO> Handle(
            ForgetPasswordappUserCommand request,
            CancellationToken cancellationToken)
        {
            var token = await _identityUser.GeneratePasswordResetTokenForEmailAsync(request.Dto.Email);

            return new ForgetPasswordappUserResultDTO
            {
                Message = "Password reset request was received.",
                ResetToken = token
            };
        }
    }
}
