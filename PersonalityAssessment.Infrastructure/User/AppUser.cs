using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalityAssessment.Infrastructure.User
{
    public class AppUser : IdentityUser
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }

        public string? ProfileImageUrl { get; set; }

        public bool IsGoogleAccount { get; set; }

        public bool GoogleSignInCompatible { get; set; }

        public string? GoogleId { get; set; }

        //   public String FullName { get; set; }
        [NotMapped]
        public string FullName => FirstName + " " + LastName;

    }
}
