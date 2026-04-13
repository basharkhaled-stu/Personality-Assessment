using FluentValidation;
using PersonalityAssessment.Application.Features.Options.Commands;

namespace PersonalityAssessment.Application.Features.Options.Commands.Validators
{
    public class UpdateAppUserProfileValidator : AbstractValidator<UpdateAppUserProfileCommand>
    {
        public UpdateAppUserProfileValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.Dto)
                .Must(d =>
                    !string.IsNullOrWhiteSpace(d.NewUsername)
                    || d.FirstName != null
                    || d.LastName != null
                    || !string.IsNullOrWhiteSpace(d.NewEmail)
                    || !string.IsNullOrWhiteSpace(d.NewPassword))
                .WithMessage("At least one profile field must be provided.");

            When(x => !string.IsNullOrWhiteSpace(x.Dto.NewPassword), () =>
            {
                RuleFor(x => x.Dto.CurrentPassword)
                    .NotEmpty()
                    .WithMessage("Current password is required when setting a new password.");
            });

            When(x => !string.IsNullOrWhiteSpace(x.Dto.NewUsername), () =>
            {
                RuleFor(x => x.Dto.NewUsername!)
                    .MinimumLength(4)
                    .MaximumLength(20);
            });

            When(x => x.Dto.FirstName != null, () =>
            {
                RuleFor(x => x.Dto.FirstName!)
                    .NotEmpty()
                    .MaximumLength(100);
            });

            When(x => x.Dto.LastName != null, () =>
            {
                RuleFor(x => x.Dto.LastName!)
                    .NotEmpty()
                    .MaximumLength(100);
            });

            When(x => !string.IsNullOrWhiteSpace(x.Dto.NewEmail), () =>
            {
                RuleFor(x => x.Dto.NewEmail!)
                    .EmailAddress();
            });

            When(x => !string.IsNullOrWhiteSpace(x.Dto.NewPassword), () =>
            {
                RuleFor(x => x.Dto.NewPassword!)
                    .MinimumLength(6);
            });
        }
    }
}
