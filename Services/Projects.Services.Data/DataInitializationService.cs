using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Projects.Data;
using Projects.Data.Common;

namespace Projects.Services.Data
{
    public class DataInitializationService : IDataInitializationService
    {
        private readonly IDbQueryRunner dbQueryRunner;

        public DataInitializationService(IDbQueryRunner dbQueryRunner)
        {
            this.dbQueryRunner = dbQueryRunner;
        }

        public async Task InitializeNewDataAsync()
        {
            await this.dbQueryRunner.RunQueryAsync("EXEC initialize_database");
        }
    }
}
