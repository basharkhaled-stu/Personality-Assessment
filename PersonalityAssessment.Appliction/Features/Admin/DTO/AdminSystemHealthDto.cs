namespace PersonalityAssessment.Application.Features.Admin.DTO
{
    public class AdminSystemHealthDto
    {
        public string ApiStatus { get; set; } = "healthy";
        public string DatabaseStatus { get; set; } = string.Empty;
        public bool DatabaseConnected { get; set; }
        public string? DatabaseError { get; set; }
        public long WorkingSetBytes { get; set; }
        public long GcTotalMemoryBytes { get; set; }
        public double UptimeSeconds { get; set; }
        public long TotalHttpRequests { get; set; }
    }
}
