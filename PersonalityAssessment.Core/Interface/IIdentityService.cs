namespace PersonalityAssessment.Core.Interface
{
    public interface IIdentityService
    {
        Task<string> GetFullNameAsync(string userId);
    }
}
