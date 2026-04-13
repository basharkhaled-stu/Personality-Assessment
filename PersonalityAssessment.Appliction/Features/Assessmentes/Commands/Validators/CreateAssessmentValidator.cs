using FluentValidation;

namespace PersonalityAssessment.Application.Features.Assessmentes.Commands.Validators
{
    public class CreateAssessmentValidator : AbstractValidator<CreateAssessmentCommand>
    {
        public CreateAssessmentValidator()
        {

            RuleFor(x => x.DTO.Title)
                 .NotEmpty()
                 .WithMessage("Title Is required");


            RuleFor(x => x.DTO.AssessmentStatusId)
               .GreaterThan(0)
            .WithMessage("AssessmentStatusId must be greater than 0");


            RuleFor(x => x.DTO.AssessmentTypeId)
            .GreaterThan(0)
            .WithMessage("AssessmentTypeId must be greater than 0");


        }

    }

}
