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
            CreateMap<UpdateQuestionDTO, Question>();

            // BUG FIX: null-safe navigation
            CreateMap<Question, ReadQuestionDTO>()
                .ForMember(d => d.AssessmentName,
                    o => o.MapFrom(s => s.Assessment != null ? s.Assessment.Title : ""))
                .ForMember(d => d.QuestionTypeName,
                    o => o.MapFrom(s => s.QuestionType != null ? s.QuestionType.Name : ""));

            CreateMap<Question, AdmainReadQuestionDTO>()
                .ForMember(d => d.AssessmentName,
                    o => o.MapFrom(s => s.Assessment != null ? s.Assessment.Title : ""))
                .ForMember(d => d.QuestionTypeName,
                    o => o.MapFrom(s => s.QuestionType != null ? s.QuestionType.Name : ""));
        }
    }
}
