using MediatR;
using PersonalityAssessment.Core.Interface;

namespace PersonalityAssessment.Application.Features.Options.Commands.Handlers
{
    public class CreateappUserCommandHandler
        : IRequestHandler<CreateappUserCommand, bool>
    {
        private readonly IIdentityUser _IdentityUser;

        public CreateappUserCommandHandler(IIdentityUser IdentityUser)
        {

            _IdentityUser = IdentityUser;
        }



        public async Task<bool> Handle
            (CreateappUserCommand request,
            CancellationToken cancellationToken)
        {

            var googleSignInCompatible = EmailDomainValidation.IsGmailCompatibleDomain(request.DTO.Email);
            var result = await _IdentityUser.Register(request.DTO.FirstName, request.DTO.LastName,
                request.DTO.Email, request.DTO.Username, request.DTO.Password, googleSignInCompatible);
            return result;
        }


    }
}
