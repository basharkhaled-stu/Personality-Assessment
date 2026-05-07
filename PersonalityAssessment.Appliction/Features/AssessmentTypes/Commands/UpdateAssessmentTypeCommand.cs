using MediatR;
using PersonalityAssessment.Application.Features.AssessmentTypes.DTO;

namespace PersonalityAssessment.Application.Features.AssessmentTypes.Commands
{
    public record UpdateAssessmentTypeCommand(int id, UpdateAssessmentTypeDTO dto)
       : IRequest<ReadAssessmentTypeDTO>;
}
