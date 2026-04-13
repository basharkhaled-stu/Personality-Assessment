using MediatR;
using PersonalityAssessment.Application.Features.UsersAssessments.DTO;

namespace PersonalityAssessment.Application.Features.UsersAssessments.Commands
{
    public record class CreateUsersAssessmentCommand(CreateUsersAssessmentDTO DTO, string UserId)
        : IRequest<ReadUsersAssessmentDTO>;

}
