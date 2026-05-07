namespace PersonalityAssessment.Application.Services
{
    public interface IGoogleLoginAppService
    {
        Task<string?> SignInOrRegisterAsync(string idToken, CancellationToken cancellationToken = default);
    }
}
