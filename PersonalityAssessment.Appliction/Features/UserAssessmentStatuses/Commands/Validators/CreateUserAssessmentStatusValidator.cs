using FluentValidation;

namespace PersonalityAssessment.Application.Features.UserAssessmentStatuses.Commands.Validators
{
    public class CreateUserAssessmentStatusValidator : AbstractValidator<CreateUserAssessmentStatusCommand>
    {
        public CreateUserAssessmentStatusValidator()
        {

            RuleFor(x => x.DTO.Name)
                .NotEmpty()
                .WithMessage("Name Is Required");




        }

    }

}
