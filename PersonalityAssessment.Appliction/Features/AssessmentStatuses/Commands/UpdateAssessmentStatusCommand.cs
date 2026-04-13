using MediatR;
using PersonalityAssessment.Application.Features.AssessmentStatuses.DTO;

namespace PersonalityAssessment.Application.Features.AssessmentStatuses.Commands
{
    public record UpdateAssessmentStatusCommand(int id, UpdateAssessmentStatusDTO dto)
       : IRequest<ReadAssessmentStatusDTO>;
}
