namespace Projects.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class TimeLog
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public DateTime Date { get; set; }

        public float HoursWorked { get; set; }
    }
}
