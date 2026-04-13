namespace PersonalityAssessment.Application.Features.Options.DTO
{
    public class UpdateProfileappUserDTO
    {
        public string? NewUsername { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NewEmail { get; set; }
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
    }
}
