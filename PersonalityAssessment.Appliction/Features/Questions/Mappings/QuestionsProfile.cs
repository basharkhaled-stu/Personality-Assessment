using AutoMapper;
using PersonalityAssessment.Application.Features.Questions.DTO;
using PersonalityAssessment.Core.Entities;
namespace PersonalityAssessment.Application.Features.Questions.Mappings
{
    public class QuestionsProfile : Profile
    {
        public QuestionsProfile()
        {

            CreateMap<CreateQuestionDTO, Question>();
            CreateMap<Question, ReadQuestionDTO>()
                .ForMember(a => a.AssessmentName,
                opt => opt.MapFrom(a => a.Assessment.Title))
                .ForMember(qt => qt.QuestionTypeName,
                opt => opt.MapFrom(qt => qt.QuestionType.Name));








            CreateMap<Question, AdmainReadQuestionDTO>()
                .ForMember(a => a.AssessmentName,
                opt => opt.MapFrom(a => a.Assessment.Title))
                .ForMember(qt => qt.QuestionTypeName,
                opt => opt.MapFrom(qt => qt.QuestionType.Name));

            CreateMap<UpdateQuestionDTO, Question>();






        }





    }
}
