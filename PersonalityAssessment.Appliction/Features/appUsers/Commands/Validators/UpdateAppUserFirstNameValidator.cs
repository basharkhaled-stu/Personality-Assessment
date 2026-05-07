using FluentValidation;
using PersonalityAssessment.Application.Features.appUsers.Commands;

namespace PersonalityAssessment.Application.Features.appUsers.Commands.Validators
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
