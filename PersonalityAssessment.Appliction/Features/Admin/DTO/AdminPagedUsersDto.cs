namespace PersonalityAssessment.Application.Features.Admin.DTO
{
    public class AdminPagedUsersDto
    {
        public List<AdminUserSummaryDto> Items { get; set; } = new();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
