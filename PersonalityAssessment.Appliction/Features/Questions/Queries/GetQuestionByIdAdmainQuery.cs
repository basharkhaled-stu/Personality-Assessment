using MediatR;
using PersonalityAssessment.Application.Features.Questions.DTO;

namespace PersonalityAssessment.Application.Features.Questions.Queries
{
    public record class GetQuestionByIdAdmainQuery(int id) :
        IRequest<AdmainReadQuestionDTO>;

}
