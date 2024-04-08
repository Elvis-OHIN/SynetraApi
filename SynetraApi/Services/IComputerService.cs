using SynetraUtils.Models.DataManagement;

namespace SynetraApi.Services
{
    public interface IComputerService
    {
        Task<List<Computer>> GetComputersAsync();
        Task<Computer> GetComputerByIdAsync(int id);
        Task<Computer> GetComputerByFootPrintAsync(string footPrint);
        Task<Computer> CreateComputerAsync(Computer Computer);
        Task<Computer> UpdateComputerAsync(int id, Computer Computer);
        Task<Computer> UpdateComputerFootPrintAsync(int id, string footPrint);
        Task<bool> DeleteComputerAsync(int id);
    }
}
