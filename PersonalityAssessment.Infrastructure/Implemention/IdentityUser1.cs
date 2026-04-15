using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PersonalityAssessment.Core.Interface;
using PersonalityAssessment.Infrastructure.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace PersonalityAssessment.Infrastructure.Implemention
{
    public class IdentityUser1 : IIdentityUser
    {
        private readonly UserManager<AppUser> _User;
        private readonly PasswordHasher<AppUser> _PasswordHasher;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityUser1(
            UserManager<AppUser> User,
            PasswordHasher<AppUser> passwordHasher,
            IConfiguration config,
            IWebHostEnvironment webHostEnvironment,
            RoleManager<IdentityRole> roleManager)
        {
            _User = User;
            _PasswordHasher = passwordHasher;
            _config = config;
            _webHostEnvironment = webHostEnvironment;
            _roleManager = roleManager;
        }

        /*  public async Task<bool> Register(string FirstName, string LastName, string Email, string UserName, string Password, bool googleSignInCompatible = false)
          {
              var isHaveUser = await _User.FindByNameAsync(UserName);

              if (isHaveUser != null)
                  return false;

              AppUser user = new AppUser()
              {
                  FirstName = FirstName,
                  LastName = LastName,
                  Email = Email,
                  UserName = UserName,
                  GoogleSignInCompatible = googleSignInCompatible
              };

              var result = await _User.CreateAsync(user, Password);

              return result.Succeeded;
          }*/
        public async Task<bool> Register(string FirstName, string LastName, string Email, string UserName, string Password, bool googleSignInCompatible = false)
        {
            var isHaveUser = await _User.FindByNameAsync(UserName);
            if (isHaveUser != null)
                return false;

            var isHaveEmail = await _User.FindByEmailAsync(Email);
            if (isHaveEmail != null)
                return false;

            AppUser user = new AppUser()
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                UserName = UserName,
                GoogleSignInCompatible = googleSignInCompatible
            };

            var result = await _User.CreateAsync(user, Password);

            if (!result.Succeeded)
                return false;

            var roleResult = await _User.AddToRoleAsync(user, "User");

            if (!roleResult.Succeeded)
                return false;

            return true;
        }
        public async Task<bool> RegisterAdmin(string FirstName, string LastName, string Email, string UserName, string Password, bool googleSignInCompatible = false)
        {
            var isHaveUser = await _User.FindByNameAsync(UserName);
            if (isHaveUser != null)
                return false;

            var isHaveEmail = await _User.FindByEmailAsync(Email);
            if (isHaveEmail != null)
                return false;

            AppUser user = new AppUser()
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                UserName = UserName,
                GoogleSignInCompatible = googleSignInCompatible
            };

            var result = await _User.CreateAsync(user, Password);

            if (!result.Succeeded)
                return false;

            var roleResult = await _User.AddToRoleAsync(user, "Admin");

            if (!roleResult.Succeeded)
                return false;

            return true;
        }




        public async Task<bool> Update(string UserName, string Password)
        {
            var user = await _User.FindByNameAsync(UserName);

            if (user == null)
                return false;

            user.PasswordHash = _PasswordHasher.HashPassword(user, Password);

            var result = await _User.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task<string> Login(string UserName, string Password)
        {
            var user = await _User.FindByNameAsync(UserName);

            if (user == null)
                return null;

            var result = await _User.CheckPasswordAsync(user, Password);

            if (!result)
                return null;

            return await GenerateJwtToken(user); // FIX
        }

        public async Task<string> LoginByEmail(string Email, string Password)
        {
            var user = await _User.FindByEmailAsync(Email);

            if (user == null)
                return null;

            var result = await _User.CheckPasswordAsync(user, Password);

            if (!result)
                return null;

            return await GenerateJwtToken(user); // FIX
        }

        public async Task<string?> GeneratePasswordResetTokenForEmailAsync(string email)
        {
            var user = await _User.FindByEmailAsync(email);
            if (user == null)
                return null;

            return await _User.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<string?> LoginOrRegisterWithGoogleAsync(string googleId, string email, string? name, bool emailVerified)
        {
            const string provider = "Google";

            var byLogin = await _User.FindByLoginAsync(provider, googleId);
            if (byLogin != null)
                return await GenerateJwtToken(byLogin); // FIX

            var byEmail = await _User.FindByEmailAsync(email);
            if (byEmail != null)
            {
                if (!emailVerified)
                    return null;

                var addLogin = await _User.AddLoginAsync(byEmail, new UserLoginInfo(provider, googleId, provider));
                if (!addLogin.Succeeded)
                    return null;

                byEmail.GoogleId = googleId;
                byEmail.IsGoogleAccount = true;
                byEmail.GoogleSignInCompatible = IsGmailOrGoogleMailDomain(email);
                await _User.UpdateAsync(byEmail);

                return await GenerateJwtToken(byEmail); // FIX
            }

            if (!emailVerified)
                return null;

            var (firstName, lastName) = SplitDisplayName(name);
            var baseUserName = BuildUsernameFromEmail(email);
            var userName = await EnsureUniqueUserNameAsync(baseUserName);
            var randomPassword = GenerateOpaquePassword();

            var newUser = new AppUser
            {
                UserName = userName,
                Email = email,
                EmailConfirmed = true,
                FirstName = firstName,
                LastName = lastName,
                IsGoogleAccount = true,
                GoogleId = googleId,
                GoogleSignInCompatible = IsGmailOrGoogleMailDomain(email)
            };

            var create = await _User.CreateAsync(newUser, randomPassword);
            if (!create.Succeeded)
                return null;

            var addNewLogin = await _User.AddLoginAsync(newUser, new UserLoginInfo(provider, googleId, provider)); // FIX (القوس)

            if (!addNewLogin.Succeeded)
                return null;

            return await GenerateJwtToken(newUser); // FIX
        }

        private static bool IsGmailOrGoogleMailDomain(string email)
        {
            var at = email.LastIndexOf('@');
            if (at < 0 || at == email.Length - 1)
                return false;

            var domain = email[(at + 1)..].Trim().ToLowerInvariant();
            return domain is "gmail.com" or "googlemail.com";
        }

        private static (string FirstName, string LastName) SplitDisplayName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return ("Google", "User");

            var trimmed = name.Trim();
            var spaceIdx = trimmed.IndexOf(' ');

            if (spaceIdx < 0)
                return (trimmed, trimmed);

            return (trimmed[..spaceIdx], trimmed[(spaceIdx + 1)..].Trim());
        }

        private static string BuildUsernameFromEmail(string email)
        {
            var local = email.Split('@')[0];
            var sanitized = Regex.Replace(local, @"[^a-zA-Z0-9]", "");

            if (string.IsNullOrEmpty(sanitized))
                sanitized = "user";

            if (sanitized.Length > 16)
                sanitized = sanitized[..16];

            return sanitized;
        }

        private async Task<string> EnsureUniqueUserNameAsync(string baseName)
        {
            var candidate = baseName;
            var n = 0;

            while (await _User.FindByNameAsync(candidate) != null)
            {
                n++;
                var suffix = n.ToString();
                var maxLen = Math.Max(1, 20 - suffix.Length);
                candidate = baseName.Length > maxLen ? baseName[..maxLen] + suffix : baseName + suffix;

                if (candidate.Length > 20)
                    candidate = candidate[..20];
            }

            return candidate;
        }

        private static string GenerateOpaquePassword()
        {
            var bytes = RandomNumberGenerator.GetBytes(24);
            var core = Convert.ToBase64String(bytes);
            return core + "Aa1!";
        }

        /* public async Task<string> GenerateJwtToken(AppUser user)
         {
             var jwtSettings = _config.GetSection("JwtSettings");

             var roles = await _User.GetRolesAsync(user); // FIX

             var claims = new List<Claim>
             {
                 new Claim(ClaimTypes.Name, user.UserName),
                 new Claim(ClaimTypes.NameIdentifier, user.Id),
             };

             foreach (var role in roles)
             {
                 claims.Add(new Claim(ClaimTypes.Role, role)); // FIX
             }

             var key = new SymmetricSecurityKey(
                 Encoding.UTF8.GetBytes(jwtSettings["Secret"])
             );

             var creds = new SigningCredentials(
                 key,
                 SecurityAlgorithms.HmacSha256
             );

             var token = new JwtSecurityToken(
                 issuer: jwtSettings["Issuer"],
                 audience: jwtSettings["Audience"],
                 claims: claims,
                 expires: DateTime.Now.AddMinutes(
                     Convert.ToDouble(jwtSettings["ExpiresInMinutes"])
                 ),
                 signingCredentials: creds
             );

             return new JwtSecurityTokenHandler().WriteToken(token);
         }*/

        public async Task<string> GenerateJwtToken(AppUser user)
        {
            var jwtSettings = _config.GetSection("JwtSettings");

            var roles = await _User.GetRolesAsync(user);

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.NameIdentifier, user.Id)
    };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Secret"])
            );

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                    Convert.ToDouble(jwtSettings["ExpiresInMinutes"])
                ),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> UpdateUsernameAsync(string userId, string newUsername)
        {
            var user = await _User.FindByIdAsync(userId);
            if (user == null)
                return false;

            var taken = await _User.FindByNameAsync(newUsername);
            if (taken != null && taken.Id != userId)
                return false;

            var result = await _User.SetUserNameAsync(user, newUsername);
            return result.Succeeded;
        }

        public async Task<bool> UpdateFirstNameAsync(string userId, string firstName)
        {
            var user = await _User.FindByIdAsync(userId);
            if (user == null)
                return false;

            user.FirstName = firstName;
            var result = await _User.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> UpdateLastNameAsync(string userId, string lastName)
        {
            var user = await _User.FindByIdAsync(userId);
            if (user == null)
                return false;

            user.LastName = lastName;
            var result = await _User.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> UpdateEmailAsync(string userId, string newEmail)
        {
            var user = await _User.FindByIdAsync(userId);
            if (user == null)
                return false;

            var taken = await _User.FindByEmailAsync(newEmail);
            if (taken != null && taken.Id != userId)
                return false;

            var result = await _User.SetEmailAsync(user, newEmail);
            return result.Succeeded;
        }

        public async Task<bool> UpdatePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = await _User.FindByIdAsync(userId);
            if (user == null)
                return false;

            var result = await _User.ChangePasswordAsync(user, currentPassword, newPassword);
            return result.Succeeded;
        }

        public async Task<bool> UpdateProfileImageAsync(string userId, Stream imageStream, string fileExtension)
        {
            var user = await _User.FindByIdAsync(userId);
            if (user == null)
                return false;

            var ext = string.IsNullOrWhiteSpace(fileExtension)
                ? ".bin"
                : (fileExtension.StartsWith(".") ? fileExtension : "." + fileExtension);

            var webRoot = _webHostEnvironment.WebRootPath
                ?? Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot");

            var uploadsDir = Path.Combine(webRoot, "uploads", "profiles");
            Directory.CreateDirectory(uploadsDir);

            var physicalPath = Path.Combine(uploadsDir, userId + ext);

            await using (var fileStream = new FileStream(physicalPath, FileMode.Create, FileAccess.Write))
            {
                await imageStream.CopyToAsync(fileStream);
            }

            user.ProfileImageUrl = "/uploads/profiles/" + userId + ext;

            var updateResult = await _User.UpdateAsync(user);
            return updateResult.Succeeded;
        }

        public async Task<bool> UpdateProfileAsync(
            string userId,
            string? newUsername,
            string? firstName,
            string? lastName,
            string? newEmail,
            string? currentPassword,
            string? newPassword)
        {
            var user = await _User.FindByIdAsync(userId);
            if (user == null)
                return false;

            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                if (string.IsNullOrWhiteSpace(currentPassword))
                    return false;

                var pwdResult = await _User.ChangePasswordAsync(user, currentPassword, newPassword);
                if (!pwdResult.Succeeded)
                    return false;
            }

            if (!string.IsNullOrWhiteSpace(newUsername))
            {
                var taken = await _User.FindByNameAsync(newUsername);
                if (taken != null && taken.Id != userId)
                    return false;

                var unResult = await _User.SetUserNameAsync(user, newUsername);
                if (!unResult.Succeeded)
                    return false;
            }

            if (!string.IsNullOrWhiteSpace(newEmail))
            {
                var takenEmail = await _User.FindByEmailAsync(newEmail);
                if (takenEmail != null && takenEmail.Id != userId)
                    return false;

                var emResult = await _User.SetEmailAsync(user, newEmail);
                if (!emResult.Succeeded)
                    return false;
            }

            if (firstName != null)
                user.FirstName = firstName;

            if (lastName != null)
                user.LastName = lastName;

            if (firstName != null || lastName != null)
            {
                var result = await _User.UpdateAsync(user);
                return result.Succeeded;
            }

            return true;
        }


    }
}
