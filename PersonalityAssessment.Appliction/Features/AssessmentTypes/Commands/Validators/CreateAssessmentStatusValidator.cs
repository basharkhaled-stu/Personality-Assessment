using FluentValidation;

namespace PersonalityAssessment.Application.Features.AssessmentTypes.Commands.Validators
{
    public class CreateAssessmentStatusValidator : AbstractValidator<CreateAssessmentTypeCommand>
    {
        public CreateAssessmentStatusValidator()
        {

            RuleFor(x => x.DTO.Name)
           .NotEmpty().WithMessage("Name Is required");
        }

    }

}
