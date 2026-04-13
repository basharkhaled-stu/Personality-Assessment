using MediatR;
using PersonalityAssessment.Application.Features.AssessmentStatuses.DTO;

namespace PersonalityAssessment.Application.Features.AssessmentStatuses.Commands
{
    public record class CreateAssessmentStatusCommand(CreateAssessmentStatusDTO DTO)
        : IRequest<ReadAssessmentStatusDTO>;

}
