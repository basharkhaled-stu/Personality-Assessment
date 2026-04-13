using MediatR;
using PersonalityAssessment.Application.Features.AssessmentStatuses.DTO;

namespace PersonalityAssessment.Application.Features.AssessmentStatuses.Queries
{
    public record class GetAssessmentStatusByIdQuery(int id) :
        IRequest<ReadAssessmentStatusDTO>;

}
