using FluentValidation;
using PersonalityAssessment.Application.Features.appUsers.Commands;

namespace PersonalityAssessment.Application.Features.appUsers.Commands.Validators
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
