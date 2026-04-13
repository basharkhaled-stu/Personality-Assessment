using MediatR;
using PersonalityAssessment.Application.Features.Options.Commands;
using PersonalityAssessment.Core.Interface;

namespace PersonalityAssessment.Application.Features.Users.Commands.UpdateUser
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