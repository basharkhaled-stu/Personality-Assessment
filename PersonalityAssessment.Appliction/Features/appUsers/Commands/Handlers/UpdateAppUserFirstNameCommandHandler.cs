using MediatR;
using PersonalityAssessment.Application.Features.Options.Commands;
using PersonalityAssessment.Core.Interface;

namespace PersonalityAssessment.Application.Features.Options.Commands.Handlers
{
    public class UpdateAppUserFirstNameCommandHandler
        : IRequestHandler<UpdateAppUserFirstNameCommand, bool>
    {
        private readonly IIdentityUser _identityUser;

        public UpdateAppUserFirstNameCommandHandler(IIdentityUser identityUser)
        {
            _identityUser = identityUser;
        }

        public Task<bool> Handle(UpdateAppUserFirstNameCommand request, CancellationToken cancellationToken)
        {
            return _identityUser.UpdateFirstNameAsync(request.UserId, request.Dto.FirstName);
        }
    }
}
