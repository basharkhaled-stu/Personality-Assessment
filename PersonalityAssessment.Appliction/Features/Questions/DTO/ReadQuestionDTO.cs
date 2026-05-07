namespace PersonalityAssessment.Application.Features.Questions.DTO
{


    public class ReadQuestionDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int DisplayOrder { get; set; }

        public string AssessmentName { get; set; }


        public string QuestionTypeName { get; set; }



    }
}
