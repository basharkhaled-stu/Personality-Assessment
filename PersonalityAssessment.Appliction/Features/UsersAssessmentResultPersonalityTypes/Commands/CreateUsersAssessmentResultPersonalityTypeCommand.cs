using MediatR;
using PersonalityAssessment.Application.Features.UsersAssessmentResultPersonalityTypes.DTO;

namespace PersonalityAssessment.Application.Features.UsersAssessmentResultPersonalityTypes.Commands
{
    public record class CreateUsersAssessmentResultPersonalityTypeCommand(CreateUsersAssessmentResultPersonalityTypeDTO DTO)
        : IRequest<ReadUsersAssessmentResultPersonalityTypeDTO>;

}
