using AutoMapper;

using Command;

using Domains.DBModels;

namespace LandHubWebService.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateUserCommand, User>().ForMember(d => d.FirstName,
                o => o.MapFrom(s => s.FirstName));
        }

    }
}
