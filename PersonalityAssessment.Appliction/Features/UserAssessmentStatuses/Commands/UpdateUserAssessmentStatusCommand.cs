using MediatR;
using PersonalityAssessment.Application.Features.UserAssessmentStatuses.DTO;

namespace PersonalityAssessment.Application.Features.UserAssessmentStatuses.Commands
{
    public record UpdateUserAssessmentStatusCommand(int id, UpdateUserAssessmentStatusDTO dto)
       : IRequest<bool>;
}
