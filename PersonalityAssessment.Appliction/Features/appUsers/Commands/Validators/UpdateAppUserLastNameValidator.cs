using FluentValidation;
using PersonalityAssessment.Application.Features.Options.Commands;

namespace PersonalityAssessment.Application.Features.Options.Commands.Validators
{
    public class UpdateAppUserLastNameValidator : AbstractValidator<UpdateAppUserLastNameCommand>
    {
        public UpdateAppUserLastNameValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.Dto.LastName)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
