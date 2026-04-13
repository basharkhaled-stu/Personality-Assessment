namespace PersonalityAssessment.Application.Features.UsersAssessmentResults.DTO
{
    public class AdmainReadUsersAssessmentResultDTO
    {
        public int Id { get; set; }
        public string UsersAssessmentName { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? CreatedByUserId { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedByUserId { get; set; }



    }
}
