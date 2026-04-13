using MediatR;
using PersonalityAssessment.Application.Features.PersonalityTypes.DTO;

namespace PersonalityAssessment.Application.Features.PersonalityTypes.Queries
{
    public record class GetPersonalityTypeByIdAdmainQuery(int id) :
        IRequest<AdmainPersonalityTypeDTO>;

}
