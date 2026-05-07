using FluentValidation;

namespace PersonalityAssessment.Application.Features.AssessmentStatuses.Commands.Validators
{
    public class CreateAssessmentStatusValidator : AbstractValidator<CreateAssessmentStatusCommand>
    {
        public CreateAssessmentStatusValidator()
        {

            RuleFor(x => x.DTO.Name)
           .NotEmpty().WithMessage("Name Is required");
        }

    }

}
