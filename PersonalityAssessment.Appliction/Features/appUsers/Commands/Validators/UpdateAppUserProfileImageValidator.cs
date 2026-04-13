using FluentValidation;
using PersonalityAssessment.Application.Features.Options.Commands;

namespace PersonalityAssessment.Application.Features.Options.Commands.Validators
{
    public class UpdateAppUserProfileImageValidator : AbstractValidator<UpdateAppUserProfileImageCommand>
    {
        private static readonly string[] AllowedExtensions =
        {
            ".jpg", ".jpeg", ".png", ".gif", ".webp"
        };

        public UpdateAppUserProfileImageValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.ImageStream)
                .NotNull();

            RuleFor(x => x.FileSize)
                .InclusiveBetween(1, 5 * 1024 * 1024)
                .WithMessage("Image must be between 1 byte and 5 MB.");

            RuleFor(x => x.OriginalFileName)
                .NotEmpty()
                .Must(name =>
                {
                    var ext = Path.GetExtension(name).ToLowerInvariant();
                    return AllowedExtensions.Contains(ext);
                })
                .WithMessage("Allowed image types: jpg, jpeg, png, gif, webp.");
        }
    }
}
