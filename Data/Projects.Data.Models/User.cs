namespace Projects.Data.Models
{
    using System.Collections.Generic;

    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public ICollection<TimeLog> TimeLogs { get; set; } = new HashSet<TimeLog>();
    }
}
