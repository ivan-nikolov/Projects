namespace Projects.Web.Models
{
    using System;
    using Projects.Services.Mapping;
    using Projects.Services.Models;

    public class UsersPageQueryModel : IMapTo<PageQueryModel>
    {
        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public DateTime DateFrom { get; set; } = DateTime.MinValue;

        public DateTime DateTo { get; set; } = DateTime.UtcNow;

        public string SortBy { get; set; } = "FirstName";
    }
}
