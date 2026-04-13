using FluentValidation;
using PersonalityAssessment.Application.Features.Options.Commands;

namespace PersonalityAssessment.Application.Features.Options.Commands.Validators
{
    public class UpdateAppUserEmailValidator : AbstractValidator<UpdateAppUserEmailCommand>
    {
        public UpdateAppUserEmailValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.Dto.NewEmail)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
