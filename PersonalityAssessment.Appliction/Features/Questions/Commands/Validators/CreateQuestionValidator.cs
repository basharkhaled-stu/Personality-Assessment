using FluentValidation;

namespace PersonalityAssessment.Application.Features.Questions.Commands.Validators
{
    public class CreateQuestionValidator : AbstractValidator<CreateQuestionCommand>
    {
        public CreateQuestionValidator()
        {
            RuleFor(x => x.DTO.Text)
                .NotEmpty()
                .WithMessage("Text Is Required");

            RuleFor(x => x.DTO.QuestionTypeId)
               .GreaterThan(0)
               .WithMessage("QuestionTypeId Must Be Creater Than 0");


            RuleFor(x => x.DTO.AssessmentId)
               .GreaterThan(0)
               .WithMessage("AssessmentId Must Be Creater Than 0");
        }

    }

}
