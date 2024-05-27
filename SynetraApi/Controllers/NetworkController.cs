using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SynetraApi.Data;
using SynetraUtils.Models.DataManagement;

namespace SynetraApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NetworkController : Controller
    {
        private readonly DataContext _context;

        public NetworkController(DataContext context)
        {
            _context = context;
        }

        // GET: Network
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.NetworkInfo.Include(n => n.Computer);
            return Ok(await dataContext.ToListAsync());
        }

        // GET: Network/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var networkInfo = await _context.NetworkInfo
                .Include(n => n.Computer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (networkInfo == null)
            {
                return NotFound();
            }

            return Ok(networkInfo);
        }

        // POST: Network/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,ComputerId,CarteMere,Type,MACAddress,IPAddress,SubnetMask,DefaultGateway,DNServers,Status")] NetworkInfo networkInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(networkInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return Ok(networkInfo);
        }

       

        // POST: Network/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ComputerId,CarteMere,Type,MACAddress,IPAddress,SubnetMask,DefaultGateway,DNServers,Status")] NetworkInfo networkInfo)
        {
            if (id != networkInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(networkInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NetworkInfoExists(networkInfo.Id))
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
            
            return Ok(networkInfo);
        }

      
        // POST: Network/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var networkInfo = await _context.NetworkInfo.FindAsync(id);
            if (networkInfo != null)
            {
                _context.NetworkInfo.Remove(networkInfo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NetworkInfoExists(int id)
        {
            return _context.NetworkInfo.Any(e => e.Id == id);
        }
    }
}
