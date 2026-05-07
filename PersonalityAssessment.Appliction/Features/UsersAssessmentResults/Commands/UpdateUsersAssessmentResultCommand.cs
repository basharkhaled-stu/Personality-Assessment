using MediatR;
using PersonalityAssessment.Application.Features.UsersAssessmentResults.DTO;

namespace PersonalityAssessment.Application.Features.UsersAssessmentResults.Commands
{
    public record UpdateUsersAssessmentResultCommand(int id, UpdateUsersAssessmentResultDTO dto)
       : IRequest<bool>;
}
