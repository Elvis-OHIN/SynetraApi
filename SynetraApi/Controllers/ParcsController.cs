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
    public class ParcsController : Controller
    {
        private readonly SynetraApiContext _context;

        public ParcsController(SynetraApiContext context)
        {
            _context = context;
        }

        // GET: Parcs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Parcs.ToListAsync());
        }

        // GET: Parcs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parcs = await _context.Parcs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parcs == null)
            {
                return NotFound();
            }

            return View(parcs);
        }

        // GET: Parcs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parcs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IsActive,IsEnable,CreatedDate,UpdatedDate")] Parcs parcs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parcs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parcs);
        }

        // GET: Parcs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parcs = await _context.Parcs.FindAsync(id);
            if (parcs == null)
            {
                return NotFound();
            }
            return View(parcs);
        }

        // POST: Parcs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IsActive,IsEnable,CreatedDate,UpdatedDate")] Parcs parcs)
        {
            if (id != parcs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parcs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParcsExists(parcs.Id))
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
            return View(parcs);
        }

        // GET: Parcs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parcs = await _context.Parcs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parcs == null)
            {
                return NotFound();
            }

            return View(parcs);
        }

        // POST: Parcs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parcs = await _context.Parcs.FindAsync(id);
            if (parcs != null)
            {
                _context.Parcs.Remove(parcs);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParcsExists(int id)
        {
            return _context.Parcs.Any(e => e.Id == id);
        }
    }
}
