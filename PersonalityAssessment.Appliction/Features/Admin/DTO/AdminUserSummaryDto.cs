namespace PersonalityAssessment.Application.Features.Admin.DTO
{
    public class AdminUserSummaryDto
    {
        public string Id { get; set; } = string.Empty;
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool IsGoogleAccount { get; set; }
    }
}
