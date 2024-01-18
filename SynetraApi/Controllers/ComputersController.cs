using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SynetraApi.Data;
using SynetraApi.Models;

namespace SynetraApi.Controllers
{
    public class ComputersController : Controller
    {
        private readonly DataContext _context;

        public ComputersController(DataContext context)
        {
            _context = context;
        }

        // GET: Computers
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Computer.Include(c => c.Room);
            return View(await dataContext.ToListAsync());
        }

        // GET: Computers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computers = await _context.Computer
                .Include(c => c.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (computers == null)
            {
                return NotFound();
            }

            return View(computers);
        }

        // GET: Computers/Create
        public IActionResult Create()
        {
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Id");
            return View();
        }

        // POST: Computers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IDProduct,OperatingSystem,Os,CarteMere,GPU,RoomId,Statut,IsActive,IsEnable,CreatedDate,UpdatedDate")] Computer computers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(computers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Id", computers.RoomId);
            return View(computers);
        }

        // GET: Computers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computers = await _context.Computer.FindAsync(id);
            if (computers == null)
            {
                return NotFound();
            }
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Id", computers.RoomId);
            return View(computers);
        }

        // POST: Computers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IDProduct,OperatingSystem,Os,CarteMere,GPU,RoomId,Statut,IsActive,IsEnable,CreatedDate,UpdatedDate")] Computer computers)
        {
            if (id != computers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(computers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComputersExists(computers.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Id", computers.RoomId);
            return View(computers);
        }

        // GET: Computers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computers = await _context.Computer
                .Include(c => c.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (computers == null)
            {
                return NotFound();
            }

            return View(computers);
        }

        // POST: Computers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var computers = await _context.Computer.FindAsync(id);
            if (computers != null)
            {
                _context.Computer.Remove(computers);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComputersExists(int id)
        {
            return _context.Computer.Any(e => e.Id == id);
        }
    }
}
