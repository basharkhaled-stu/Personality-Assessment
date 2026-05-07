using MediatR;
using PersonalityAssessment.Application.Features.UsersAssessmentResultPersonalityTypes.DTO;

namespace PersonalityAssessment.Application.Features.UsersAssessmentResultPersonalityTypes.Queries
{
    public record class GetUsersAssessmentResultPersonalityTypeByIdQuery(int id) :
        IRequest<ReadUsersAssessmentResultPersonalityTypeDTO>;

}
