using FluentValidation;
using PersonalityAssessment.Application.Features.appUsers.Commands;

namespace PersonalityAssessment.Application.Features.appUsers.Commands.Validators
{
    public class ForgetPasswordappUserCommandValidator : AbstractValidator<ForgetPasswordappUserCommand>
    {
        public ForgetPasswordappUserCommandValidator()
        {
            RuleFor(x => x.Dto.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
