using MediatR;
using PersonalityAssessment.Application.Features.PersonalityTypes.DTO;

namespace PersonalityAssessment.Application.Features.PersonalityTypes.Queries
{
    public record class GetPersonalityTypeByIdQuery(int id) :
        IRequest<ReadPersonalityTypeDTO>;

}
