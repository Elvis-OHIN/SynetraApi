using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SynetraApi.Data;
using SynetraApi.Models;
using SynetraApi.Services;
using SynetraUtils.Models.DataManagement;

namespace SynetraApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : Controller
    {
        private readonly DataContext _context;
        private readonly IRoomService _roomService;

        public RoomsController(DataContext context, IRoomService roomService)
        {
            _context = context;
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _roomService.GetRoomsAsync());
        }

        // GET: Rooms/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            var room = await _roomService.GetRoomByIdAsync((int)id);
            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

   
        // POST: Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,RoomsId,IsActive,IsEnable,CreatedDate,UpdatedDate")] Room rooms)
        {
            if (ModelState.IsValid)
            {
                await _roomService.CreateRoomAsync(rooms);
                return Ok(rooms);
            }
            return Ok(rooms);
        }

      
        // POST: Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,RoomsId,IsActive,IsEnable,CreatedDate,UpdatedDate")] Room rooms)
        {
            if (ModelState.IsValid)
            {
                var RoomUpdate = new Room();
                try
                {
                    RoomUpdate = await _roomService.UpdateRoomAsync(id, rooms);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomsExists(rooms.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok(RoomUpdate);
            }
            return Ok(rooms);
        }

      

        // POST: Rooms/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _roomService.DeleteRoomAsync(id);
            }
            catch
            {
                return NotFound();
            }
            return Ok(true);
        }

        private bool RoomsExists(int id)
        {
            return _context.Room.Any(e => e.Id == id);
        }
    }
}
