using MediatR;
using PersonalityAssessment.Application.Features.Options.DTO;

namespace PersonalityAssessment.Application.Features.Options.Queries
{
    public record class GetappUserByEmailQuery(LoginByEmailappUserDTO Dto)
        : IRequest<string>;
}
