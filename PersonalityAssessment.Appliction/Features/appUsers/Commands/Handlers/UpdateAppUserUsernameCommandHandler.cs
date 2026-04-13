using MediatR;
using PersonalityAssessment.Application.Features.Options.Commands;
using PersonalityAssessment.Core.Interface;

namespace PersonalityAssessment.Application.Features.Options.Commands.Handlers
{
    public class UpdateAppUserUsernameCommandHandler
        : IRequestHandler<UpdateAppUserUsernameCommand, bool>
    {
        private readonly IIdentityUser _identityUser;

        public UpdateAppUserUsernameCommandHandler(IIdentityUser identityUser)
        {
            _identityUser = identityUser;
        }

        public Task<bool> Handle(UpdateAppUserUsernameCommand request, CancellationToken cancellationToken)
        {
            return _identityUser.UpdateUsernameAsync(request.UserId, request.Dto.NewUsername);
        }
    }
}
