namespace Projects.Services.Models
{
    using System;

    public class PageQueryModel
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public string SortBy { get; set; }
    }
}
