namespace Projects.Web.Models
{
    using Projects.Services.Mapping;
    using Projects.Services.Models.Project;

    public class TopProjectsViewModel : IMapFrom<TopProjectDto>
    { 
        public int UserId { get; set; }

        public string FullName { get; set; }

        public string ProjectName { get; set; }

        public double Hours { get; set; }
    }
}
