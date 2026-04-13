namespace PersonalityAssessment.Core.Interface
{
    public interface IIdentityUser
    {
        Task<bool> Register(string FirstName, string LastName, string Email, string UserName, string Password, bool googleSignInCompatible = false);
        Task<string> Login(string UserName, string Password);
        Task<string> LoginByEmail(string Email, string Password);
        Task<string?> LoginOrRegisterWithGoogleAsync(string googleId, string email, string? name, bool emailVerified);
        Task<string?> GeneratePasswordResetTokenForEmailAsync(string email);
        Task<bool> Update(string UserName, string Password);

        Task<bool> UpdateUsernameAsync(string userId, string newUsername);
        Task<bool> UpdateFirstNameAsync(string userId, string firstName);
        Task<bool> UpdateLastNameAsync(string userId, string lastName);
        Task<bool> UpdateEmailAsync(string userId, string newEmail);
        Task<bool> UpdatePasswordAsync(string userId, string currentPassword, string newPassword);
        Task<bool> UpdateProfileImageAsync(string userId, Stream imageStream, string fileExtension);
        Task<bool> UpdateProfileAsync(
            string userId,
            string? newUsername,
            string? firstName,
            string? lastName,
            string? newEmail,
            string? currentPassword,
            string? newPassword);
    }
}
