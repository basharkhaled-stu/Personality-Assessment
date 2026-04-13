namespace PersonalityAssessment.Application.Features.UsersAssessmentResultPersonalityTypes.DTO
{
    public class AdmainReadUsersAssessmentResultPersonalityTypeDTO
    {
        public int Id { get; set; }
        public string PersonalityTypeName { get; set; }
        public string UsersAssessmentResultName { get; set; }

        public decimal Score { get; set; }
        public decimal Rank { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? CreatedByUserId { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedByUserId { get; set; }


    }
}
