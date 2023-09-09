namespace Projects.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Projects.Data.Models;
    using Projects.Data.Repositories;
    using Projects.Services.Mapping;
    using Projects.Services.Models.Project;

    public class ProjectsService : IProjectsService
    {
        private readonly IRepository<TimeLog> timeLogsRepo;

        public ProjectsService(IRepository<TimeLog> timeLogsRepo)
        {
            this.timeLogsRepo = timeLogsRepo;
        }

        public IEnumerable<T> GetTop10ProjectsAsync<T>(DateTime? dateFrom, DateTime? dateTo)
        {
            return this.timeLogsRepo.AllAsNoTracking()
                .Where(x => dateFrom.Value.Date <= x.Date.Date && dateTo.Value.Date >= x.Date.Date)
                .Select(x => new TopProjectDto()
                {
                    UserId = x.UserId,
                    FullName = x.User.FirstName + " " + x.User.LastName,
                    ProjectName = x.Project.Name,
                    Hours = x.HoursWorked,
                })
                .AsEnumerable()
                .GroupBy(x => new { x.UserId, x.ProjectName })
                .OrderByDescending(x => x.Sum(y => y.Hours))
                .Select(x => new TopProjectDto() 
                {
                    UserId = x.Key.UserId,
                    ProjectName = x.Key.ProjectName,
                    FullName = x.FirstOrDefault().FullName,
                    Hours = x.Sum(y => y.Hours)
                })
                .Take(10)
                .Select(x => x.To<T>())
                .ToList();
        }
        public T GetTopProjectHoursByUserId<T>(int userId, DateTime? dateFrom, DateTime? dateTo)
        {
            return this.timeLogsRepo.AllAsNoTracking()
                .Where(x => x.UserId == userId && x.Date.Date >= dateFrom.Value.Date && x.Date.Date <= dateTo.Value.Date)
                .Select(x => new TopProjectDto()
                {
                    UserId = x.UserId,
                    FullName = x.User.FirstName + " " + x.User.LastName,
                    ProjectName = x.Project.Name,
                    Hours = x.HoursWorked,
                })
                .AsEnumerable()
                .GroupBy(x => new { x.ProjectName })
                .OrderByDescending(x => x.Sum(y => y.Hours))
                .Select(x => new TopProjectDto()
                {
                    UserId = x.FirstOrDefault().UserId,
                    ProjectName = x.Key.ProjectName,
                    FullName = x.FirstOrDefault().FullName,
                    Hours = x.Sum(y => y.Hours)
                })
                .Select(x => x.To<T>())
                .FirstOrDefault();
        }

        public T GetTotalHoursByUser<T>(int userId, DateTime? dateFrom, DateTime? dateTo)
        {
            return this.timeLogsRepo.AllAsNoTracking()
                .Where(x => x.UserId == userId && x.Date.Date >= dateFrom.Value.Date && x.Date.Date <= dateTo.Value.Date)
                .Select(x => new TopProjectDto()
                {
                    UserId = x.UserId,
                    FullName = x.User.FirstName + " " + x.User.LastName,
                    ProjectName = x.Project.Name,
                    Hours = x.HoursWorked,
                })
                .AsEnumerable()
                .GroupBy(x => new { x.UserId })
                .OrderByDescending(x => x.Sum(y => y.Hours))
                .Select(x => new TopProjectDto()
                {
                    UserId = x.Key.UserId,
                    ProjectName = x.FirstOrDefault().ProjectName,
                    FullName = x.FirstOrDefault().FullName,
                    Hours = x.Sum(y => y.Hours)
                })
                .Select(x => x.To<T>())
                .FirstOrDefault();
        }
    }
}
