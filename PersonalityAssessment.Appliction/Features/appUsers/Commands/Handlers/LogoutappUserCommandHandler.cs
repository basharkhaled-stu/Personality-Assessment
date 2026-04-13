using MediatR;
using PersonalityAssessment.Application.Features.Options.Commands;
using PersonalityAssessment.Application.Features.Options.DTO;

namespace PersonalityAssessment.Application.Features.Options.Commands.Handlers
{
    /// <summary>
    /// JWT access tokens are not revoked server-side here. Clients must delete the token from storage
    /// (memory, secure storage, cookies). The token remains valid until it expires; for immediate
    /// invalidation you would add a refresh-token blocklist, versioned signing keys, or short-lived tokens.
    /// </summary>
    public class LogoutappUserCommandHandler
        : IRequestHandler<LogoutappUserCommand, LogoutappUserResultDTO>
    {
        private const string LogoutMessage =
            "Logout acknowledged. Remove the JWT from the client. This API does not maintain a server-side token blocklist; " +
            "the access token stays valid until its expiry unless you implement token revocation separately.";

        public Task<LogoutappUserResultDTO> Handle(LogoutappUserCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new LogoutappUserResultDTO { Message = LogoutMessage });
        }
    }
}
