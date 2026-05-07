using AutoMapper;
using PersonalityAssessment.Application.Features.UsersAssessmentResultPersonalityTypes.DTO;
using PersonalityAssessment.Core.Entities;
namespace PersonalityAssessment.Application.Features.UsersAssessmentResultPersonalityTypes.Mappings
{
    public class UsersAssessmentResultPersonalityTypesProfile : Profile
    {
        public UsersAssessmentResultPersonalityTypesProfile()
        {

            CreateMap<CreateUsersAssessmentResultPersonalityTypeDTO, UsersAssessmentResultPersonalityType>();

            CreateMap<UpdateUsersAssessmentResultPersonalityTypeDTO, UsersAssessmentResultPersonalityType>();
            CreateMap<UsersAssessmentResultPersonalityType, ReadUsersAssessmentResultPersonalityTypeDTO>()
                .ForMember(x => x.UsersAssessmentResultName,
                opt => opt.MapFrom(x => x.UsersAssessmentResult.usersAssessment.UserId)
                )
                .ForMember(x => x.PersonalityTypeName,
                opt => opt.MapFrom(x => x.PersonalityType.Name));










            CreateMap<UsersAssessmentResultPersonalityType, AdmainReadUsersAssessmentResultPersonalityTypeDTO>()
                 .ForMember(x => x.UsersAssessmentResultName,
                opt => opt.MapFrom(x => x.UsersAssessmentResult.usersAssessment.UserId))

                .ForMember(x => x.PersonalityTypeName,
                opt => opt.MapFrom(x => x.PersonalityType.Name));










        }





    }
}
