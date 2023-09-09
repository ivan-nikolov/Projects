namespace Projects.Services.Models.Users
{
    using Projects.Data.Models;
    using Projects.Services.Mapping;

    public class UserDto : IMapFrom<User>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
