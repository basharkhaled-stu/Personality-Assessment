using FluentValidation;

namespace PersonalityAssessment.Application.Features.UsersAssessmentResults.Commands.Validators
{
    public class CreateUsersAssessmentResultValidator : AbstractValidator<CreateUsersAssessmentResultCommand>
    {
        public CreateUsersAssessmentResultValidator()
        {

            RuleFor(x => x.DTO.UsersAssessmentId)
           .NotEmpty().WithMessage("UsersAssessmentId Must be Greate Than 0");
        }

    }

}
