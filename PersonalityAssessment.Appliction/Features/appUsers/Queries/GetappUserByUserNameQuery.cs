using MediatR;
using PersonalityAssessment.Application.Features.Options.DTO;

namespace PersonalityAssessment.Application.Features.Options.Queries
{
    public record class GetappUserByUserNameQuery(LoginappUserDTO dto) :
        IRequest<string>;

}
