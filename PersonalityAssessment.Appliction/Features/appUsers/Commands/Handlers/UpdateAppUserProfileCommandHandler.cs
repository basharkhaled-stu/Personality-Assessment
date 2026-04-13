using MediatR;
using PersonalityAssessment.Application.Features.Options.Commands;
using PersonalityAssessment.Core.Interface;

namespace PersonalityAssessment.Application.Features.Options.Commands.Handlers
{
    public class UpdateAppUserProfileCommandHandler
        : IRequestHandler<UpdateAppUserProfileCommand, bool>
    {
        private readonly IIdentityUser _identityUser;

        public UpdateAppUserProfileCommandHandler(IIdentityUser identityUser)
        {
            _identityUser = identityUser;
        }

        public Task<bool> Handle(UpdateAppUserProfileCommand request, CancellationToken cancellationToken)
        {
            var d = request.Dto;
            return _identityUser.UpdateProfileAsync(
                request.UserId,
                d.NewUsername,
                d.FirstName,
                d.LastName,
                d.NewEmail,
                d.CurrentPassword,
                d.NewPassword);
        }
    }
}
