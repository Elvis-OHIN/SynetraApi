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
    [Route("api/[controller]")]
    public class ShareScreensController : Controller
    {
        private readonly DataContext _context;

        public ShareScreensController(DataContext context)
        {
            _context = context;
        }

        // GET: ShareScreens
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            
            return Ok(await _context.ShareScreen.ToListAsync());
        }

        // GET: ShareScreens/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shareScreen = await _context.ShareScreen
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shareScreen == null)
            {
                return NotFound();
            }

            return Ok(shareScreen);
        }

        // POST: ShareScreens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,ComputerID,ImageData,CreatedDate,UpdatedDate")] ShareScreen shareScreen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shareScreen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
          
            return Ok(shareScreen);
        }

        // POST: ShareScreens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, ShareScreen shareScreen)
        {
           

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shareScreen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShareScreenExists(shareScreen.Id))
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
          
            return Ok(shareScreen);
        }

        // POST: ShareScreens/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shareScreen = await _context.ShareScreen.FindAsync(id);
            if (shareScreen != null)
            {
                _context.ShareScreen.Remove(shareScreen);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShareScreenExists(int id)
        {
            return _context.ShareScreen.Any(e => e.Id == id);
        }
    }
}
