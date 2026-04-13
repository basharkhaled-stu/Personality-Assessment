using FluentValidation;

namespace PersonalityAssessment.Application.Features.QuestionTypes.Commands.Validators
{
    public class CreateQuestionTypeValidator : AbstractValidator<CreateQuestionTypeCommand>
    {
        public CreateQuestionTypeValidator()
        {

            RuleFor(x => x.DTO.Name)
           .NotEmpty().WithMessage("Name Is required");
        }

    }

}
