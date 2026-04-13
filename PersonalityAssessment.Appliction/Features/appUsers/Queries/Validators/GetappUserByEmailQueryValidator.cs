using FluentValidation;
using PersonalityAssessment.Application.Features.Options.Queries;

namespace PersonalityAssessment.Application.Features.Options.Queries.Validators
{
    public class GetappUserByEmailQueryValidator : AbstractValidator<GetappUserByEmailQuery>
    {
        public GetappUserByEmailQueryValidator()
        {
            RuleFor(x => x.Dto.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Dto.Password)
                .NotEmpty();
        }
    }
}
