using SynetraUtils.Models.DataManagement;

namespace SynetraApi.Services
{
    public interface IRoomService
    {
        Task<List<Room>> GetRoomsAsync();
        Task<Room> GetRoomByIdAsync(int id);
        Task<Room> CreateRoomAsync(Room Room);
        Task<Room> UpdateRoomAsync(int id, Room Room);
        Task<bool> DeleteRoomAsync(int id);
    }
}
