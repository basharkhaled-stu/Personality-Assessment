namespace PersonalityAssessment.Application.Features.UsersAssessments.DTO
{
    public class AdmainReadUsersAssessmentDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string UserAssessmentStatusName { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }


        public DateTime CreatedAt { get; set; }
        public string? CreatedByUserId { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedByUserId { get; set; }






    }
}
