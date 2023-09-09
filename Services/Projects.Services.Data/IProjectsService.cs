namespace Projects.Services.Data
{
    using System;
    using System.Collections.Generic;

    public interface IProjectsService
    {
        IEnumerable<T> GetTop10ProjectsAsync<T>(DateTime? dateFrom, DateTime? dateTo);

        T GetTopProjectHoursByUserId<T>(int userId, DateTime? dateFrom, DateTime? dateTo);

        T GetTotalHoursByUser<T>(int userId, DateTime? dateFrom, DateTime? dateTo);
    }
}
