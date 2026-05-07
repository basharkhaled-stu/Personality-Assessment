using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using PersonalityAssessment.Core.Interface;

namespace PersonalityAssessment.Infrastructure.Implemention
{
    public class GoogleIdTokenValidator : IGoogleIdTokenValidator
    {
        private readonly IConfiguration _configuration;

        public GoogleIdTokenValidator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<GoogleValidatedUserInfo?> ValidateAsync(string idToken, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(idToken))
                return null;

            var clientId = _configuration["GoogleAuth:ClientId"];
            if (string.IsNullOrWhiteSpace(clientId))
                return null;

            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { clientId }
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

                if (string.IsNullOrWhiteSpace(payload.Subject) || string.IsNullOrWhiteSpace(payload.Email))
                    return null;

                return new GoogleValidatedUserInfo
                {
                    GoogleId = payload.Subject,
                    Email = payload.Email,
                    Name = payload.Name,
                    EmailVerified = payload.EmailVerified
                };
            }
            catch (InvalidJwtException)
            {
                return null;
            }
        }
    }
}
