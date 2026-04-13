using FluentValidation;

namespace PersonalityAssessment.Application.Features.UserAnswers.Commands.Validators
{
    public class CreateUserAnswerValidator : AbstractValidator<CreateUserAnswerCommand>
    {
        public CreateUserAnswerValidator()
        {
            RuleFor(x => x.DTO.OptionId)
                .GreaterThan(0)
                .WithMessage("OptionId Must Be Creater Than 0");

            RuleFor(x => x.DTO.QuestionId)
               .GreaterThan(0)
               .WithMessage("QuestionId Must Be Creater Than 0");


            RuleFor(x => x.DTO.UsersAssessmentId)
               .GreaterThan(0)
               .WithMessage("UsersAssessmentId Must Be Creater Than 0");
        }

    }

}
