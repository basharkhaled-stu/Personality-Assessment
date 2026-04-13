using FluentValidation;
using PersonalityAssessment.Application.Features.Options.Commands;

namespace PersonalityAssessment.Application.Features.Options.Commands.Validators
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
