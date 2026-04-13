using FluentValidation;
using PersonalityAssessment.Application.Features.Options.Commands;

namespace PersonalityAssessment.Application.Features.Options.Commands.Validators
{
    public class GoogleLoginappUserCommandValidator : AbstractValidator<GoogleLoginappUserCommand>
    {
        public GoogleLoginappUserCommandValidator()
        {
            RuleFor(x => x.Dto.IdToken)
                .NotEmpty();
        }
    }
}
