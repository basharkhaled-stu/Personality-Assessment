using AutoMapper;
using PersonalityAssessment.Application.Features.Options.DTO;
using PersonalityAssessment.Core.Entities;

namespace PersonalityAssessment.Application.Features.Options.Mappings
{
    public class OptionsProfile : Profile
    {
        public OptionsProfile()
        {
            CreateMap<CreateOptionDTO, Option>();
            CreateMap<UpdateOptionDTO, Option>();

            // BUG FIX: null-safe navigation
            CreateMap<Option, ReadOptionDTO>()
                .ForMember(d => d.QuestionName,
                    o => o.MapFrom(s => s.Question != null ? s.Question.Text : ""));

            CreateMap<Option, AdmainReadOptionDTO>()
                .ForMember(d => d.QuestionName,
                    o => o.MapFrom(s => s.Question != null ? s.Question.Text : ""));
        }
    }
}
