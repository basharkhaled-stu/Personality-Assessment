using MediatR;
using PersonalityAssessment.Application.Features.Options.Commands;
using PersonalityAssessment.Core.Interface;

namespace PersonalityAssessment.Application.Features.Options.Commands.Handlers
{
    public class UpdateAppUserLastNameCommandHandler
        : IRequestHandler<UpdateAppUserLastNameCommand, bool>
    {
        private readonly IIdentityUser _identityUser;

        public UpdateAppUserLastNameCommandHandler(IIdentityUser identityUser)
        {
            _identityUser = identityUser;
        }

        public Task<bool> Handle(UpdateAppUserLastNameCommand request, CancellationToken cancellationToken)
        {
            return _identityUser.UpdateLastNameAsync(request.UserId, request.Dto.LastName);
        }
    }
}
