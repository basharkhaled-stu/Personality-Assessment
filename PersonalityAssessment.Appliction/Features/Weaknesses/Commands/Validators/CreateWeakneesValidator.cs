using FluentValidation;

namespace PersonalityAssessment.Application.Features.Weaknesses.Commands.Validators
{
    public class CreateWeakneesValidator : AbstractValidator<CreateWeaknessCommand>
    {
        public CreateWeakneesValidator()
        {

            RuleFor(x => x.DTO.Text)
           .NotEmpty().WithMessage("Text Is required");
        }

    }

}
