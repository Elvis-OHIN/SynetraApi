using SynetraUtils.Models.DataManagement;

namespace SynetraApi.Services
{
    public interface IParcService
    {
        Task<List<Parc>> GetParcsAsync();
        Task<Parc> GetParcByIdAsync(int id);
        Task<Parc> CreateParcAsync(Parc Parc);
        Task<Parc> UpdateParcAsync(int id, Parc Parc);
        Task<bool> DeleteParcAsync(int id);
    }
}
