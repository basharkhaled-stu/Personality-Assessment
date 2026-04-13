using MediatR;
using PersonalityAssessment.Application.Features.Options.Commands;
using PersonalityAssessment.Core.Interface;

namespace PersonalityAssessment.Application.Features.Options.Commands.Handlers
{
    public class UpdateAppUserEmailCommandHandler
        : IRequestHandler<UpdateAppUserEmailCommand, bool>
    {
        private readonly IIdentityUser _identityUser;

        public UpdateAppUserEmailCommandHandler(IIdentityUser identityUser)
        {
            _identityUser = identityUser;
        }

        public Task<bool> Handle(UpdateAppUserEmailCommand request, CancellationToken cancellationToken)
        {
            return _identityUser.UpdateEmailAsync(request.UserId, request.Dto.NewEmail);
        }
    }
}
