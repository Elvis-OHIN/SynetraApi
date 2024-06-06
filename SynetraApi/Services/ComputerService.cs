using Microsoft.EntityFrameworkCore;
using SynetraApi.Data;
using SynetraUtils.Models.DataManagement;
using SynetraUtils.Auth;

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

        public async Task<Computer> GetComputerByFootPrintAsync(string footPrint)
        {
            
            var computers =  _context.Computer
                .AsEnumerable()
                .Where(c => c.FootPrint == footPrint)
                .FirstOrDefault();

            return computers;
        }

        public async Task<Computer> CreateComputerConnectionAsync(int id,Connection connection)
        {
            var existingComputer = await _context.Computer.FindAsync(id);
            if (existingComputer == null)
            {
                return null;
            }
            if (existingComputer.Connections is null)
            {
                existingComputer.Connections = new List<Connection>();
            }
            existingComputer.Connections.Add(connection);
            await _context.SaveChangesAsync();
            return existingComputer;
        }

        public async Task<Computer> GetComputerConnectionAsync(int id)
        {
            var computer = _context.Computer
                .Include(u => u.Connections)
                .SingleOrDefault(u => u.Id == id);
            return computer;
        }

        public async Task<Computer> GetComputerByIdAsync(int id)
        {
            return await _context.Computer.FindAsync(id);
        }


        public async Task<List<Computer>> GetComputersAsync()
        {
            return await _context.Computer.Where(r => r.IsEnable == true).ToListAsync();
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
            existingComputer.ParcId = updatedComputer.ParcId;
            existingComputer.Statut = updatedComputer.Statut;
            existingComputer.UpdatedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return existingComputer;
        }

        public async Task<Computer> UpdateComputerFootPrintAsync(int id, string footPrint)
        {
            var existingComputer = await _context.Computer.FindAsync(id);
            if (existingComputer == null)
            {
                return null;
            }

            existingComputer.FootPrint = footPrint;
            existingComputer.UpdatedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return existingComputer;
        }

        public async Task<List<Computer>> GetComputersByParcAsync(int parcId)
        {
            var computerList = await _context.Computer.Include(p => p.Room).ToListAsync();
            computerList = await _context.Computer.Include(p => p.Parc).ToListAsync();
            computerList = await _context.Computer.Where(r => r.IsEnable == true && r.Parc.IsEnable == true && r.Parc.Id == parcId).ToListAsync();
            return computerList;
        }
    }
}
