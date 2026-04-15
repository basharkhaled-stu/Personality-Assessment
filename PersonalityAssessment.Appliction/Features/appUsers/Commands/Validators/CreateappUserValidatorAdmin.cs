using FluentValidation;

namespace PersonalityAssessment.Application.Features.appUsers.Commands.Validators
{
    public class CreateappUserValidatorAdmin : AbstractValidator<CreateappUserCommandAdmin>
    {
        public CreateappUserValidatorAdmin()
        {

            RuleFor(x => x.DTO.FirstName)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("First name is required");

            RuleFor(x => x.DTO.LastName)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Last name is required");

            RuleFor(x => x.DTO.Username)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(20)
                .WithMessage("Username must be between 4 and 20 characters");

            RuleFor(x => x.DTO.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Invalid email format")
                .Must(EmailDomainValidation.HasPlausibleMailDomain)
                .WithMessage("Email domain is not in a valid format (e.g. example.com).");

            RuleFor(x => x.DTO.Password)
                .NotEmpty()
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters");
        }

    }
}
