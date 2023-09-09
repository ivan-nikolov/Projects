namespace Projects.Services.Models.Users
{
    using System;

    public class UsersPageQueryModel : PageQueryModel
    {
        public DateTime? FromDate { get; set; } = DateTime.MinValue;

        public DateTime? ToDate { get; set; } = DateTime.UtcNow;
    }
}
