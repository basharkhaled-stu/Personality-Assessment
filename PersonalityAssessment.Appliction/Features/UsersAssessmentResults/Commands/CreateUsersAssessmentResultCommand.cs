using MediatR;
using PersonalityAssessment.Application.Features.UsersAssessmentResults.DTO;

namespace PersonalityAssessment.Application.Features.UsersAssessmentResults.Commands
{
    public record class CreateUsersAssessmentResultCommand(CreateUsersAssessmentResultDTO DTO)
        : IRequest<ReadUsersAssessmentResultDTO>;

}
