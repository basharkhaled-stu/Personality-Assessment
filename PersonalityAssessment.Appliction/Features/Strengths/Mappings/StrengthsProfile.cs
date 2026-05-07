using AutoMapper;
using PersonalityAssessment.Application.Features.Strengths.DTO;
using PersonalityAssessment.Core.Entities;

namespace PersonalityAssessment.Application.Features.Strengths.Mappings
{
    public class StrengthsProfile : Profile
    {
        public StrengthsProfile()
        {
            CreateMap<CreateStrengthDTO, Strength>();
            CreateMap<UpdateStrengthDTO, Strength>();

            // BUG FIX: null-safe navigation
            CreateMap<Strength, ReadStrengthDTO>()
                .ForMember(d => d.PersonalityTypeName,
                    o => o.MapFrom(s => s.PersonalityType != null ? s.PersonalityType.Name : ""));

            CreateMap<Strength, AdmainReadStrengthDTO>()
                .ForMember(d => d.PersonalityTypeName,
                    o => o.MapFrom(s => s.PersonalityType != null ? s.PersonalityType.Name : ""));
        }
    }
}
