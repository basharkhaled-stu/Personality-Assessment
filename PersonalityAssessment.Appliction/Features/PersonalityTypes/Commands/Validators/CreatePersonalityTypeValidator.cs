using FluentValidation;
using System.Text.RegularExpressions;

namespace PersonalityAssessment.Application.Features.PersonalityTypes.Commands.Validators
{
    public class CreatePersonalityTypeValidator : AbstractValidator<CreatePersonalityTypeCommand>
    {
        public CreatePersonalityTypeValidator()
        {

            RuleFor(x => x.DTO.Name)
                .NotEmpty()
                .WithMessage("Name Is Required");


            RuleFor(x => x.DTO.Label)
               .NotEmpty()
               .WithMessage("Label Is Required");



            RuleFor(x => x.DTO.ImageUrl)
               .NotEmpty()
               .WithMessage("ImageUrl Is Required")
               .Must(BeAValidImageUrl).WithMessage("Image URL must be a valid image link (jpg, png, gif, jpeg)");

        }
        private bool BeAValidImageUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return false;

            // Check if it's a valid URL
            if (!Uri.TryCreate(url, UriKind.Absolute, out var uriResult))
                return false;

            // Check if URL ends with common image extensions
            string pattern = @"\.(jpeg|jpg|png|gif|bmp|webp)$";
            return Regex.IsMatch(url, pattern, RegexOptions.IgnoreCase);
        }
    }

}
