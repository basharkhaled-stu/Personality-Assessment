using FluentValidation;
using PersonalityAssessment.Application.Features.appUsers.Commands;

namespace PersonalityAssessment.Application.Features.appUsers.Commands.Validators
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
