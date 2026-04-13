using MediatR;
using PersonalityAssessment.Application.Features.QuestionTypes.DTO;

namespace PersonalityAssessment.Application.Features.QuestionTypes.Queries
{
    public record class GetQuestionTypeByIdAdmainQuery(int id) :
        IRequest<AdmainReadQuestionTypeDTO>;

}
