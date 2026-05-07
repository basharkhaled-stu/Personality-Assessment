using FluentValidation;

namespace PersonalityAssessment.Application.Features.UsersAssessments.Commands.Validators
{
    public class CreateUsersAssessmentValidator : AbstractValidator<CreateUsersAssessmentCommand>
    {
        public CreateUsersAssessmentValidator()
        {

            RuleFor(x => x.DTO.UserAssessmentStatusId)
               .GreaterThan(0)
               .WithMessage("UserAssessmentStatusId Must Be Creater Than 0");
        }

    }

}
