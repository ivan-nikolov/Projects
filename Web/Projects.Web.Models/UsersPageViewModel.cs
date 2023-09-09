namespace Projects.Web.Models
{
    using System.Collections.Generic;
    using AutoMapper;
    using Projects.Services.Mapping;
    using Projects.Services.Models;

    public class UsersPageViewModel : IMapFrom<Page<UserViewModel>>, IHaveCustomMappings
    {

        public int PageIndex { get; set; }

        public int TotalPages { get; set; }

        public bool HasPreviousPage { get; set; }

        public bool HasNextPage { get; set; }

        public int PreviousPage => PageIndex - 1;

        public int NextPage => PageIndex + 1;

        public ICollection<UserViewModel> Users { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Page<UserViewModel>, UsersPageViewModel>()
                .ForMember(dest => dest.Users, o => o.MapFrom(src => src.Items));
        }
    }
}
