using FluentValidation;
using PersonalityAssessment.Application.Features.appUsers.Commands;

namespace PersonalityAssessment.Application.Features.appUsers.Commands.Validators
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
