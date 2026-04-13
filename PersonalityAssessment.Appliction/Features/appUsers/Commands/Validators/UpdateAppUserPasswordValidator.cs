using FluentValidation;
using PersonalityAssessment.Application.Features.Options.Commands;

namespace PersonalityAssessment.Application.Features.Options.Commands.Validators
{
    public class UpdateAppUserPasswordValidator : AbstractValidator<UpdateAppUserPasswordCommand>
    {
        public UpdateAppUserPasswordValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.Dto.CurrentPassword)
                .NotEmpty();

            RuleFor(x => x.Dto.NewPassword)
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}
