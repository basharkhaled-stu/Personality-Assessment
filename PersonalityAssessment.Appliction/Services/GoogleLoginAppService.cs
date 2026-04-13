using PersonalityAssessment.Core.Interface;

namespace PersonalityAssessment.Application.Services
{
    public class GoogleLoginAppService : IGoogleLoginAppService
    {
        private readonly IGoogleIdTokenValidator _googleIdTokenValidator;
        private readonly IIdentityUser _identityUser;

        public GoogleLoginAppService(
            IGoogleIdTokenValidator googleIdTokenValidator,
            IIdentityUser identityUser)
        {
            _googleIdTokenValidator = googleIdTokenValidator;
            _identityUser = identityUser;
        }

        public async Task<string?> SignInOrRegisterAsync(string idToken, CancellationToken cancellationToken = default)
        {
            var info = await _googleIdTokenValidator.ValidateAsync(idToken, cancellationToken);
            if (info == null)
                return null;

            return await _identityUser.LoginOrRegisterWithGoogleAsync(
                info.GoogleId,
                info.Email,
                info.Name,
                info.EmailVerified);
        }
    }
}
