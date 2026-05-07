using MediatR;
using PersonalityAssessment.Core.Interface;

namespace PersonalityAssessment.Application.Features.Options.Queries.Handlers
{
    public class GetByEmailappUserQueryHandler
        : IRequestHandler<GetappUserByEmailQuery, string>
    {
        private readonly IIdentityUser _identityUser;

        public GetByEmailappUserQueryHandler(IIdentityUser identityUser)
        {
            _identityUser = identityUser;
        }

        public Task<string> Handle(GetappUserByEmailQuery request, CancellationToken cancellationToken)
        {
            return _identityUser.LoginByEmail(request.Dto.Email, request.Dto.Password);
        }
    }
}
