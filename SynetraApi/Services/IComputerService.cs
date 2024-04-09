using SynetraUtils.Models.DataManagement;

namespace SynetraApi.Services
{
    public interface IComputerService
    {
        Task<List<Computer>> GetComputersAsync();
        Task<Computer> GetComputerByIdAsync(int id);
        Task<Computer> GetComputerByFootPrintAsync(string footPrint);
        Task<Computer> CreateComputerAsync(Computer computer);
        Task<Computer> UpdateComputerAsync(int id, Computer computer);
        Task<Computer> UpdateComputerFootPrintAsync(int id, string footPrint);
        Task<bool> DeleteComputerAsync(int id);
        Task<Computer> CreateComputerConnectionAsync(int id , Connection connection);
        Task<Computer> GetComputerConnectionAsync(int id);
    }
}
