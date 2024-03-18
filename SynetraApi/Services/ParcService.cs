using Microsoft.EntityFrameworkCore;
using SynetraApi.Data;
using SynetraUtils.Models.DataManagement;

namespace SynetraApi.Services
{
    public class ParcService : IParcService
    {
        private readonly DataContext _context; 

        public ParcService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Parc>> GetParcsAsync()
        {
            return await _context.Parc.Where(p => p.IsEnable == true).ToListAsync();
        }

        public async Task<Parc> GetParcByIdAsync(int id)
        {
            return await _context.Parc.FindAsync(id);
        }

        public async Task<Parc> CreateParcAsync(Parc Parc)
        {
            Parc.CreatedDate = DateTime.Now;
            Parc.IsEnable = true;
            _context.Parc.Add(Parc);
            await _context.SaveChangesAsync();

            return Parc;
        }

        public async Task<Parc> UpdateParcAsync(int id, Parc updatedParc)
        {
            var existingParc = await _context.Parc.FindAsync(id);
            if (existingParc == null)
            {
                return null;
            }

            existingParc.Name = updatedParc.Name;
            existingParc.IsActive = updatedParc.IsActive;
            existingParc.rooms = updatedParc.rooms;
            existingParc.UpdatedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return existingParc;
        }

        public async Task<bool> DeleteParcAsync(int id)
        {
            var ParcToRemove = await _context.Parc.FindAsync(id);
            if (ParcToRemove == null)
            {
                return false;
            }
            ParcToRemove.IsEnable = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
