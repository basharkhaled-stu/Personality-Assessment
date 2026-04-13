using MediatR;
using PersonalityAssessment.Application.Features.UsersAssessments.DTO;

namespace PersonalityAssessment.Application.Features.UsersAssessments.Queries
{
    public record class GetAssessmentResultQuery(int UsersAssessmentId)
        : IRequest<AssessmentResultDTO>;
}
