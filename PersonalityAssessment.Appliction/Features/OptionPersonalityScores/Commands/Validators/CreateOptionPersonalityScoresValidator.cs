using FluentValidation;

namespace PersonalityAssessment.Application.Features.OptionPersonalityScores.Commands.Validators
{
    public class CreateOptionPersonalityScoresValidator : AbstractValidator<CreateOptionPersonalityScoreCommand>
    {
        public CreateOptionPersonalityScoresValidator()
        {


            RuleFor(x => x.DTO.OptionId)
               .GreaterThan(0)
               .WithMessage("OptionId Must Be Creater Than 0");


            RuleFor(x => x.DTO.PersonalityTypeId)
               .GreaterThan(0)
               .WithMessage("PersonalityTypeId Must Be Creater Than 0");
        }

    }

}
