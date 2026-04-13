using MediatR;
using PersonalityAssessment.Application.Features.Options.Commands;
using PersonalityAssessment.Core.Interface;

namespace PersonalityAssessment.Application.Features.Options.Commands.Handlers
{
    public class UpdateAppUserPasswordCommandHandler
        : IRequestHandler<UpdateAppUserPasswordCommand, bool>
    {
        private readonly IIdentityUser _identityUser;

        public UpdateAppUserPasswordCommandHandler(IIdentityUser identityUser)
        {
            _identityUser = identityUser;
        }

        public Task<bool> Handle(UpdateAppUserPasswordCommand request, CancellationToken cancellationToken)
        {
            return _identityUser.UpdatePasswordAsync(
                request.UserId,
                request.Dto.CurrentPassword,
                request.Dto.NewPassword);
        }
    }
}
