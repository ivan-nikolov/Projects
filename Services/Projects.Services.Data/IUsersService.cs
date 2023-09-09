namespace Projects.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Projects.Services.Models;

    public interface IUsersService
    {
        Task<Page<T>> GetUsers<T>(PageQueryModel pageQuery);

        Task<IEnumerable<T>> GetTop10Users<T>(DateTime? dateFrom, DateTime? dateTo);
    }
}
