

using MediatR;
using PersonalityAssessment.Core.Interface;

namespace PersonalityAssessment.Application.Features.appUsers.Commands.Handlers
{
    public class CreateappUserAdminCommandHandler
        : IRequestHandler<CreateappUserCommandAdmin, bool>
    {
        private readonly IIdentityUser _IdentityUser;

        public CreateappUserAdminCommandHandler(IIdentityUser IdentityUser)
        {

            _IdentityUser = IdentityUser;
        }





        public async Task<bool> Handle(CreateappUserCommandAdmin request, CancellationToken cancellationToken)
        {
            var googleSignInCompatible = EmailDomainValidation.IsGmailCompatibleDomain(request.DTO.Email);
            var result = await _IdentityUser.Register(request.DTO.FirstName, request.DTO.LastName,
                request.DTO.Email, request.DTO.Username, request.DTO.Password, googleSignInCompatible);
            return result;
        }
    }
}
