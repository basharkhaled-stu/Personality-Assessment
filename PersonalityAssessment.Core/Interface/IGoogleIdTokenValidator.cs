namespace PersonalityAssessment.Core.Interface
{
    public sealed class GoogleValidatedUserInfo
    {
        public string GoogleId { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string? Name { get; init; }
        public bool EmailVerified { get; init; }
    }

    public interface IGoogleIdTokenValidator
    {
        Task<GoogleValidatedUserInfo?> ValidateAsync(string idToken, CancellationToken cancellationToken = default);
    }
}
