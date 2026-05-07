using MediatR;
using PersonalityAssessment.Application.Features.AssessmentTypes.DTO;

namespace PersonalityAssessment.Application.Features.AssessmentTypes.Commands
{
    public record class CreateAssessmentTypeCommand(CreateAssessmentTypeDTO DTO)
        : IRequest<ReadAssessmentTypeDTO>;

}
