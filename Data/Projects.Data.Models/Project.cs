namespace Projects.Data.Models
{
    using System.Collections.Generic;

    public class Project
    {
        public int Id { get; set; }

        public string ProjectId { get; set; }

        public string Name { get; set; }

        public ICollection<TimeLog> TimeLogs { get; set; }
    }
}
