namespace PersonalityAssessment.Application.Features.UsersAssessmentResultPersonalityTypes.DTO
{
    public class UpdateUsersAssessmentResultPersonalityTypeDTO
    {

        public int Id { get; set; }
        public int PersonalityTypeId { get; set; }
        public int UsersAssessmentResultId { get; set; }

        public decimal Score { get; set; }
        public decimal Rank { get; set; }








    }
}
