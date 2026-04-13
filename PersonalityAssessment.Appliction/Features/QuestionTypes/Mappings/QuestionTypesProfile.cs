using AutoMapper;
using PersonalityAssessment.Application.Features.QuestionTypes.DTO;
using PersonalityAssessment.Core.Entities;
namespace PersonalityAssessment.Application.Features.QuestionTypes.Mappings
{
    public class QuestionTypesProfile : Profile
    {
        public QuestionTypesProfile()
        {

            CreateMap<CreateQuestionTypeDTO, QuestionType>();
            CreateMap<UpdateQuestionTypeDTO, QuestionType>();
            CreateMap<QuestionType, ReadQuestionTypeDTO>();
            CreateMap<QuestionType, AdmainReadQuestionTypeDTO>();
        }
    }
}
