using FluentValidation;

namespace PersonalityAssessment.Application.Features.Strengths.Commands.Validators
{
    public class CreateStrengthValidator : AbstractValidator<CreateStrengthCommand>
    {
        public CreateStrengthValidator()
        {

            RuleFor(x => x.DTO.Text)
           .NotEmpty().WithMessage("Text Is required");
        }

    }

}
