using SynetraUtils.Models.DataManagement;

namespace SynetraApi.Services
{
    public interface IRoomService
    {
        Task<List<Room>> GetRoomsAsync();
        Task<List<Room>> GetRoomsByParcAsync(int parcId);
        Task<Room> GetRoomByIdAsync(int id);
        Task<Room> CreateRoomAsync(Room room);
        Task<Room> UpdateRoomAsync(int id, Room room);
        Task<bool> DeleteRoomAsync(int id);
    }
}
