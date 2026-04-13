namespace PersonalityAssessment.Application.Features.Options.DTO
{
    public class UpdatePasswordappUserDTO
    {
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
