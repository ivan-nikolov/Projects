namespace Projects.Web.Models
{
    using System.Linq;
    using AutoMapper;
    using Projects.Data.Models;
    using Projects.Services.Mapping;
    using Projects.Services.Models.Project;

    public class TopUsersViewModel : IMapFrom<User>, IMapFrom<TopProjectDto>, IHaveCustomMappings
    {
        public string FullName { get; set; }

        public float Hours { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<User, TopUsersViewModel>()
                .ForMember(dest => dest.FullName, o => o.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(dest => dest.Hours, o => o.MapFrom(src => src.TimeLogs.Sum(x => x.HoursWorked)));

            configuration.CreateMap<TopProjectDto, TopUsersViewModel>()
                .ForMember(dest => dest.FullName, o => o.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Hours, o => o.MapFrom(src => src.Hours));
        }
    }
}
