using FluentValidation;

namespace PersonalityAssessment.Application.Features.Options.Commands.Validators
{
    public class CreateOptionValidator : AbstractValidator<CreateOptionCommand>
    {
        public CreateOptionValidator()
        {
            RuleFor(x => x.DTO.Text)
                .NotEmpty()
                .WithMessage("Text Is Required");

            RuleFor(x => x.DTO.QuestionId)
               .GreaterThan(0)
               .WithMessage("QuestionId Must Be Creater Than 0");

        }

    }

}
