using AutoMapper;

using Command;

using Commands;

using Domains.DBModels;

namespace LandHubWebService.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateUserCommand, User>().ForMember(d => d.FirstName,
                o => o.MapFrom(s => s.FirstName));

            CreateMap<CreateNewUserWithOrgCommand, User>()
               .ForMember(d => d.FirstName, o => o.MapFrom(s => s.CreateUserInformation.FirstName))
               .ForMember(d => d.Address, o => o.MapFrom(s => s.CreateUserInformation.Address))
               .ForMember(d => d.CountryName, o => o.MapFrom(s => s.CreateUserInformation.CountryName))
               .ForMember(d => d.DOB, o => o.MapFrom(s => s.CreateUserInformation.DOB))
               .ForMember(d => d.Roles, o => o.MapFrom(s => s.CreateUserInformation.Roles));

            CreateMap<CreateRoleCommand, Role>()
               .ForMember(d => d.Title, o => o.MapFrom(s => s.RoleName))
               .ForMember(d => d.Description, o => o.MapFrom(s => s.RoleName))
               .ForMember(d => d.IsShownInUi, o => o.MapFrom(s => true))
               .ForMember(d => d.IsActive, o => o.MapFrom(s => true));
        }

    }
}
