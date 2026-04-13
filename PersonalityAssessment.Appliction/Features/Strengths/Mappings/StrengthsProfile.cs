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
            CreateMap<Strength, ReadStrengthDTO>()
                .ForMember(x => x.PersonalityTypeName,
                opt => opt.MapFrom(x => x.PersonalityType.Name));



            CreateMap<Strength, AdmainReadStrengthDTO>()
                 .ForMember(x => x.PersonalityTypeName,
                opt => opt.MapFrom(x => x.PersonalityType.Name));
        }
    }
}
