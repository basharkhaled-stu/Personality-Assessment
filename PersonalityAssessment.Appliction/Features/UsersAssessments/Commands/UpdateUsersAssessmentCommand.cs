using MediatR;
using PersonalityAssessment.Application.Features.UsersAssessments.DTO;

namespace PersonalityAssessment.Application.Features.UsersAssessments.Commands
{
    public record UpdateUsersAssessmentCommand(int id, UpdateUsersAssessmentDTO dto, string UserId)
       : IRequest<bool>;
}
