namespace PersonalityAssessment.Application.Features.UsersAssessmentResultPersonalityTypes.DTO
{


    public class ReadUsersAssessmentResultPersonalityTypeDTO
    {
        public int Id { get; set; }
        public string PersonalityTypeName { get; set; }
        public string UsersAssessmentResultName { get; set; }

        public decimal Score { get; set; }
        public decimal Rank { get; set; }




    }
}
