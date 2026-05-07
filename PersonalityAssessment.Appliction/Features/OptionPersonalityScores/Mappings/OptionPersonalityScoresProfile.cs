using AutoMapper;
using PersonalityAssessment.Application.Features.OptionPersonalityScores.DTO;
using PersonalityAssessment.Core.Entities;

namespace PersonalityAssessment.Application.Features.OptionPersonalityScores.Mappings
{
    public class OptionPersonalityScoresProfile : Profile
    {
        public OptionPersonalityScoresProfile()
        {
            CreateMap<CreateOptionPersonalityScoreDTO, OptionPersonalityScore>();
            CreateMap<UpdateOptionPersonalityScoreDTO, OptionPersonalityScore>();

            // BUG FIX: null-safe navigation
            CreateMap<OptionPersonalityScore, ReadOptionPersonalityScoreDTO>()
                .ForMember(d => d.OptionName,
                    o => o.MapFrom(s => s.Option != null ? s.Option.Text : ""))
                .ForMember(d => d.PersonalityTypeName,
                    o => o.MapFrom(s => s.PersonalityType != null ? s.PersonalityType.Name : ""));

            CreateMap<OptionPersonalityScore, AdmainReadOptionPersonalityScoreDTO>()
                .ForMember(d => d.OptionName,
                    o => o.MapFrom(s => s.Option != null ? s.Option.Text : ""))
                .ForMember(d => d.PersonalityTypeName,
                    o => o.MapFrom(s => s.PersonalityType != null ? s.PersonalityType.Name : ""));
        }
    }
}
