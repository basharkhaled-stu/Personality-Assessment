using MediatR;
using PersonalityAssessment.Application.Features.UsersAssessmentResultPersonalityTypes.DTO;

namespace PersonalityAssessment.Application.Features.UsersAssessmentResultPersonalityTypes.Commands
{
    public record UpdateUsersAssessmentResultPersonalityTypeCommand(int id, UpdateUsersAssessmentResultPersonalityTypeDTO dto)
       : IRequest<bool>;
}
