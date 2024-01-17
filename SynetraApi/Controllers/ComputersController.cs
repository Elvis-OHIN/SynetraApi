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
    [ApiController]
    [Route("[controller]")]
    public class ComputersController : Controller
    {
        private readonly SynetraApiContext _context;

        public ComputersController(SynetraApiContext context)
        {
            _context = context;
        }

        // GET: Computers
        public async Task<IActionResult> Index()
        {
            return Ok(await _context.Computers.ToListAsync());
        }

        // GET: Computers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computers = await _context.Computers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (computers == null)
            {
                return NotFound();
            }

            return Ok(computers);
        }

        // POST: Computers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,IDProduct,OperatingSystem,Os,CarteMere,GPU,Statut,IsActive,IsEnable,CreatedDate,UpdatedDate")] Computers computers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(computers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Ok(computers);
        }

        // POST: Computers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IDProduct,OperatingSystem,Os,CarteMere,GPU,Statut,IsActive,IsEnable,CreatedDate,UpdatedDate")] Computers computers)
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
            return Ok(computers);
        }

        // POST: Computers/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var computers = await _context.Computers.FindAsync(id);
            if (computers != null)
            {
                _context.Computers.Remove(computers);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComputersExists(int id)
        {
            return _context.Computers.Any(e => e.Id == id);
        }
    }
}
