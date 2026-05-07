namespace PersonalityAssessment.Application.Features.Options.DTO
{
    public class UserProfileDTO
    {
        public string Id { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string FullName { get; set; } = "";
        public string UserName { get; set; } = "";
        public string Email { get; set; } = "";
        public string? ProfileImageUrl { get; set; }
    }
}
