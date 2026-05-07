using MediatR;
using PersonalityAssessment.Application.Features.UsersAssessments.DTO;

namespace PersonalityAssessment.Application.Features.UsersAssessments.Commands
{
    public record class SubmitAssessmentCommand(SubmitAssessmentDTO DTO)
        : IRequest<AssessmentResultDTO>;
}
