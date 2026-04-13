using MediatR;
using PersonalityAssessment.Application.Features.Options.Commands;
using PersonalityAssessment.Application.Services;

namespace PersonalityAssessment.Application.Features.Options.Commands.Handlers
{
    public class GoogleLoginappUserCommandHandler
        : IRequestHandler<GoogleLoginappUserCommand, string?>
    {
        private readonly IGoogleLoginAppService _googleLoginAppService;

        public GoogleLoginappUserCommandHandler(IGoogleLoginAppService googleLoginAppService)
        {
            _googleLoginAppService = googleLoginAppService;
        }

        public Task<string?> Handle(GoogleLoginappUserCommand request, CancellationToken cancellationToken)
        {
            return _googleLoginAppService.SignInOrRegisterAsync(request.Dto.IdToken, cancellationToken);
        }
    }
}
