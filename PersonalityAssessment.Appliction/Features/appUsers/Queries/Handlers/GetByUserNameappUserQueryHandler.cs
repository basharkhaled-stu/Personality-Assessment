using MediatR;
using PersonalityAssessment.Core.Interface;

namespace PersonalityAssessment.Application.Features.Options.Queries.Handlers
{
    public class GetByUserNameappUserQueryHandler :
        IRequestHandler<GetappUserByUserNameQuery, string>
    {
        private readonly IIdentityUser _IdentityUser;

        public GetByUserNameappUserQueryHandler(IIdentityUser identityUser)
        {
            _IdentityUser = identityUser;
        }

        public async Task<string> Handle(GetappUserByUserNameQuery request, CancellationToken cancellationToken)
        {
            var tokent = await _IdentityUser.Login(request.dto.Username, request.dto.Password);
            return tokent;
        }
    }
}
