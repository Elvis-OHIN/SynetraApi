using Microsoft.EntityFrameworkCore;
using SynetraApi.Data;
using SynetraUtils.Models.DataManagement;

namespace SynetraApi.Services
{
    public class RoomService : IRoomService
    {
        private readonly DataContext _context;
        public RoomService(DataContext context)
        {
            _context = context;
        }

        public async Task<Room> CreateRoomAsync(Room Room)
        {
            Room.CreatedDate = DateTime.Now;
            Room.IsEnable = true;
            _context.Room.Add(Room);
            await _context.SaveChangesAsync();

            return Room;
        }

        public async Task<bool> DeleteRoomAsync(int id)
        {
            var RoomToRemove = await _context.Room.FindAsync(id);
            if (RoomToRemove == null)
            {
                return false;
            }
            RoomToRemove.IsEnable = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Room> GetRoomByIdAsync(int id)
        {
            return await _context.Room.FindAsync(id);
        }

        public async Task<List<Room>> GetRoomsAsync()
        {
            var roomList = await _context.Room.Include(p => p.Parc).ToListAsync();
            roomList = await _context.Room.Where(r => r.IsEnable == true && r.Parc.IsEnable == true).ToListAsync();
            return roomList;
        }

        public async Task<List<Room>> GetRoomsByParcAsync(int parcId)
        {
            var roomList = await _context.Room.Include(p => p.Parc).ToListAsync();
            roomList = await _context.Room.Where(r => r.IsEnable == true && r.Parc.IsEnable == true && r.Parc.Id == parcId).ToListAsync();
            return roomList;
        }

        public bool ParcEnable(Parc? parc)
        {
            if (parc is null)
            {
                return false;
            }
            if (parc.IsEnable == false)
            {
                return false;
            }
            return true;
        }


        public async Task<Room> UpdateRoomAsync(int id, Room updatedRoom)
        {
            var existingRoom = await _context.Room.FindAsync(id);
            if (existingRoom == null)
            {
                return null;
            }

            existingRoom.Name = updatedRoom.Name;
            existingRoom.ParcId = updatedRoom.ParcId;
            existingRoom.IsActive = updatedRoom.IsActive;
            existingRoom.UpdatedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return existingRoom;
        }
    }
}
