namespace Projects.Services.Data
{
    using System.Threading.Tasks;

    public interface IDataInitializationService
    {
        Task InitializeNewDataAsync();
    }
}
