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
    public class LogsController : Controller
    {
        private readonly SynetraApiContext _context;

        public LogsController(SynetraApiContext context)
        {
            _context = context;
        }

        // GET: Logs
        public async Task<IActionResult> Index()
        {
            return Ok(await _context.Logs.ToListAsync());
        }

        // GET: Logs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logs = await _context.Logs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logs == null)
            {
                return NotFound();
            }

            return Ok(logs);
        }

        
        // POST: Logs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,UserId,Action,SqlRequest,CreatedDate")] Logs logs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(logs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Ok(logs);
        }

      

        // POST: Logs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Action,SqlRequest,CreatedDate")] Logs logs)
        {
            if (id != logs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(logs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LogsExists(logs.Id))
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
            return Ok(logs);
        }

 

        // POST: Logs/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var logs = await _context.Logs.FindAsync(id);
            if (logs != null)
            {
                _context.Logs.Remove(logs);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LogsExists(int id)
        {
            return _context.Logs.Any(e => e.Id == id);
        }
    }
}
