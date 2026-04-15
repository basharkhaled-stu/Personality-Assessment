using MediatR;
using PersonalityAssessment.Application.Features.Options.DTO;

namespace PersonalityAssessment.Application.Features.appUsers.Commands
{

    public record class CreateappUserCommandAdmin(RegisterappUserDTO DTO)
       : IRequest<bool>;
}

