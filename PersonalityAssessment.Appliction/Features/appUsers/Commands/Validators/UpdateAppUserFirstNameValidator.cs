using FluentValidation;
using PersonalityAssessment.Application.Features.Options.Commands;

namespace PersonalityAssessment.Application.Features.Options.Commands.Validators
{
    public class UpdateAppUserFirstNameValidator : AbstractValidator<UpdateAppUserFirstNameCommand>
    {
        public UpdateAppUserFirstNameValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.Dto.FirstName)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
