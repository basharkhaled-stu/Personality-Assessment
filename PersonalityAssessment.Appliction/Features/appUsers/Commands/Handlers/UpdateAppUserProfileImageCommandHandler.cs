using MediatR;
using PersonalityAssessment.Application.Features.Options.Commands;
using PersonalityAssessment.Core.Interface;

namespace PersonalityAssessment.Application.Features.Options.Commands.Handlers
{
    public class UpdateAppUserProfileImageCommandHandler
        : IRequestHandler<UpdateAppUserProfileImageCommand, bool>
    {
        private readonly IIdentityUser _identityUser;

        public UpdateAppUserProfileImageCommandHandler(IIdentityUser identityUser)
        {
            _identityUser = identityUser;
        }

        public async Task<bool> Handle(UpdateAppUserProfileImageCommand request, CancellationToken cancellationToken)
        {
            var ext = Path.GetExtension(request.OriginalFileName);
            if (string.IsNullOrEmpty(ext))
                ext = ".bin";

            return await _identityUser.UpdateProfileImageAsync(request.UserId, request.ImageStream, ext);
        }
    }
}
