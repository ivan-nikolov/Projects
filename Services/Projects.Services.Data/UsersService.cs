using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Projects.Common;
using Projects.Data;
using Projects.Data.Models;
using Projects.Data.Repositories;
using Projects.Services.Mapping;
using Projects.Services.Models;
using Projects.Services.Models.Users;

namespace Projects.Services.Data
{
    public class UsersService : IUsersService
    {
        private readonly IRepository<User> usersRepo;

        public UsersService(IRepository<User> usersRepo)
        {
            this.usersRepo = usersRepo;
        }

        public async Task<Page<T>> GetUsers<T>(PageQueryModel pageQuery)
        {
            var users = this.usersRepo.AllAsNoTracking()
                .Where(x => x.TimeLogs
                    .Any(y => y.Date.Date >= pageQuery.DateFrom.Date
                        && y.Date.Date <= pageQuery.DateTo.Date))
                .OrderBy(pageQuery.SortBy)
                .To<T>();

            return await Page<T>.CreateAsync(users, pageQuery.PageIndex, pageQuery.PageSize);
        }

        public async Task<IEnumerable<T>> GetTop10Users<T>(DateTime? dateFrom, DateTime? dateTo)
        {
            return await this.usersRepo.AllAsNoTracking()
                .Where(x => x.TimeLogs
                    .Any(y => y.Date.Date >= dateFrom.Value.Date
                        && y.Date.Date <= dateTo.Value.Date))
                .OrderByDescending(x => x.TimeLogs
                    .Where(y => y.Date.Date >= dateFrom.Value.Date
                        && y.Date.Date <= dateTo.Value.Date)
                    .Sum(x => x.HoursWorked))
                .Take(10)
                .To<T>()
                .ToListAsync();
        }
    }
}
