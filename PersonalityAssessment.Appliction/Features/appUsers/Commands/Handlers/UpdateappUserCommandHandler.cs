using MediatR;
using PersonalityAssessment.Application.Features.appUsers.Commands;
using PersonalityAssessment.Core.Interface;

namespace PersonalityAssessment.Application.Features.appUsers.Commands.Handlers
{
    public class UpdateAppUserCommandHandler
        : IRequestHandler<UpdateappUserCommand, bool>
    {
        private readonly IIdentityUser _identityUser;

        public UpdateAppUserCommandHandler(IIdentityUser identityUser)
        {
            _identityUser = identityUser;
        }

        public async Task<bool> Handle(UpdateappUserCommand request, CancellationToken cancellationToken)
        {
            return await _identityUser.Update(request.dto.Username, request.dto.Password);




        }
    }
}