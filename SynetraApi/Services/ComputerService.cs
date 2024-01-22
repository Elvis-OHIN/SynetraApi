using Microsoft.EntityFrameworkCore;
using SynetraApi.Data;
using SynetraApi.Models;

namespace SynetraApi.Services
{
    public class ComputerService : IComputerService
    {
        private readonly DataContext _context;

        public ComputerService(DataContext context)
        {
            _context = context;
        }
        public async Task<Computer> CreateComputerAsync(Computer Computer)
        {
            Computer.CreatedDate = DateTime.Now;
            Computer.IsEnable = true;
            _context.Computer.Add(Computer);
            await _context.SaveChangesAsync();

            return Computer;
        }

        public async Task<bool> DeleteComputerAsync(int id)
        {
            var ComputerToRemove = await _context.Computer.FindAsync(id);
            if (ComputerToRemove == null)
            {
                return false;
            }
            ComputerToRemove.IsEnable = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Computer> GetComputerByIdAsync(int id)
        {
            return await _context.Computer.FindAsync(id);
        }

        public async Task<List<Computer>> GetComputersAsync()
        {
            return await _context.Computer.ToListAsync();
        }

        public async Task<Computer> UpdateComputerAsync(int id, Computer updatedComputer)
        {
            var existingComputer = await _context.Computer.FindAsync(id);
            if (existingComputer == null)
            {
                return null;
            }

            existingComputer.Name = updatedComputer.Name;
            existingComputer.IsActive = updatedComputer.IsActive;
            existingComputer.CarteMere = updatedComputer.CarteMere;
            existingComputer.IDProduct = updatedComputer.IDProduct;
            existingComputer.GPU = updatedComputer.GPU;
            existingComputer.OperatingSystem = updatedComputer.OperatingSystem;
            existingComputer.Os = updatedComputer.Os;
            existingComputer.RoomId = updatedComputer.RoomId;
            existingComputer.Statut = updatedComputer.Statut;
            existingComputer.UpdatedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return existingComputer;
        }
    }
}
