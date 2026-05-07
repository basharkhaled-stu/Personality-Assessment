using FluentValidation;

namespace PersonalityAssessment.Application.Features.UsersAssessmentResultPersonalityTypes.Commands.Validators
{
    public class CreateUsersAssessmentResultPersonalityTypeValidator : AbstractValidator<CreateUsersAssessmentResultPersonalityTypeCommand>
    {
        public CreateUsersAssessmentResultPersonalityTypeValidator()
        {
            RuleFor(x => x.DTO.UsersAssessmentResultId)
                 .GreaterThan(0)
                .WithMessage("UsersAssessmentResultId Must Be Greater than 0");

            RuleFor(x => x.DTO.PersonalityTypeId)
                .GreaterThan(0)
               .WithMessage("PersonalityTypeId Must Be Greater than 0");

            RuleFor(x => x.DTO.Score)
              .GreaterThan(0)
             .WithMessage("Score Must Be Greater than 0");

            RuleFor(x => x.DTO.Rank)
             .GreaterThan(0)
            .WithMessage("Rank Must Be Greater than 0");



        }

    }

}
