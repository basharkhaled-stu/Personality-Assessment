using FluentValidation;
using PersonalityAssessment.Application.Features.Options.Commands;

namespace PersonalityAssessment.Application.Features.Options.Commands.Validators
{
    public class UpdateAppUserUsernameValidator : AbstractValidator<UpdateAppUserUsernameCommand>
    {
        public UpdateAppUserUsernameValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.Dto.NewUsername)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(20);
        }
    }
}
