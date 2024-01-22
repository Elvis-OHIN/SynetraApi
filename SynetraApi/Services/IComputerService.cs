using SynetraApi.Models;

namespace SynetraApi.Services
{
    public interface IComputerService
    {
        Task<List<Computer>> GetComputersAsync();
        Task<Computer> GetComputerByIdAsync(int id);
        Task<Computer> CreateComputerAsync(Computer Computer);
        Task<Computer> UpdateComputerAsync(int id, Computer Computer);
        Task<bool> DeleteComputerAsync(int id);
    }
}
