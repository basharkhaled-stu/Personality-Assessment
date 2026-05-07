namespace PersonalityAssessment.Application.Features.UsersAssessments.DTO
{


    public class ReadUsersAssessmentDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string UserAssessmentStatusName { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }










    }
}
