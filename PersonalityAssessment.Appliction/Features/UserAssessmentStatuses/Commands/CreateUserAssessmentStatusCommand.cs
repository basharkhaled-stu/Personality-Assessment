using MediatR;
using PersonalityAssessment.Application.Features.UserAssessmentStatuses.DTO;

namespace PersonalityAssessment.Application.Features.UserAssessmentStatuses.Commands
{
    public record class CreateUserAssessmentStatusCommand(CreateUserAssessmentStatusDTO DTO)
        : IRequest<ReadUserAssessmentStatusDTO>;

}
