namespace PersonalityAssessment.Application.Features.Admin.DTO
{
    public class AdminUserDetailDto
    {
        public string Id { get; set; } = string.Empty;
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool IsGoogleAccount { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public IReadOnlyList<string> Roles { get; set; } = Array.Empty<string>();
        public DateTime? CreatedAtApprox { get; set; }
    }
}
