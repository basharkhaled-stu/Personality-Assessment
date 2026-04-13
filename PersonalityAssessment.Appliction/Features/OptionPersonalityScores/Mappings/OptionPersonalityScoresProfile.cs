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
            CreateMap<OptionPersonalityScore, ReadOptionPersonalityScoreDTO>()
                .ForMember(a => a.OptionName,
                opt => opt.MapFrom(a => a.Option.Text))
                .ForMember(qt => qt.PersonalityTypeName,
                opt => opt.MapFrom(qt => qt.PersonalityType.Name));








            CreateMap<OptionPersonalityScore, AdmainReadOptionPersonalityScoreDTO>()
              .ForMember(a => a.OptionName,
                opt => opt.MapFrom(a => a.Option.Text))
                .ForMember(qt => qt.PersonalityTypeName,
                opt => opt.MapFrom(qt => qt.PersonalityType.Name));


            CreateMap<UpdateOptionPersonalityScoreDTO,OptionPersonalityScore>();



        }





    }
}
