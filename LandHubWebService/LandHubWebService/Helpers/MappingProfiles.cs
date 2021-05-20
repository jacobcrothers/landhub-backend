using AutoMapper;

using Commands;

using Domains.DBModels;

namespace LandHubWebService.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateUserCommand, ApplicationUser>()
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.FirstName))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.LastName))
                .ForMember(d => d.DOB, o => o.MapFrom(s => s.DOB))
                .ForMember(d => d.CountryName, o => o.MapFrom(s => s.CountryName))
                .ForMember(d => d.Salutation, o => o.MapFrom(s => s.Salutation))
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.Email))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
                .ForMember(d => d.Address, o => o.MapFrom(s => s.Address));

            CreateMap<CreateNewUserWithOrgCommand, User>()
               .ForMember(d => d.FirstName, o => o.MapFrom(s => s.CreateUserInformation.FirstName))
               .ForMember(d => d.Address, o => o.MapFrom(s => s.CreateUserInformation.Address))
               .ForMember(d => d.CountryName, o => o.MapFrom(s => s.CreateUserInformation.CountryName))
               .ForMember(d => d.DOB, o => o.MapFrom(s => s.CreateUserInformation.DOB));

            CreateMap<CreateRoleCommand, Role>()
               .ForMember(d => d.Title, o => o.MapFrom(s => s.RoleName))
               .ForMember(d => d.Description, o => o.MapFrom(s => s.RoleName))
               .ForMember(d => d.IsShownInUi, o => o.MapFrom(s => true))
               .ForMember(d => d.IsActive, o => o.MapFrom(s => true));
        }

    }
}
