using Microsoft.AspNetCore.Identity;
using PersonalityAssessment.Core.Interface;
using PersonalityAssessment.Infrastructure.User;

namespace PersonalityAssessment.Infrastructure.Implemention
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;

        public IdentityService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> GetFullNameAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return string.Empty;
            }

            return user.FullName;

        }
    }

}
