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
    public class ParcsController : Controller
    {
        private readonly DataContext _context;
        private readonly IParcService _parcService;

        public ParcsController(DataContext context, IParcService parcService)
        {
            _context = context;
            _parcService = parcService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllParcs()
        {
            return Ok(await _parcService.GetParcsAsync());
        }

        // GET: Parcs/Get/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetParc(int id)
        {
            var parc = await _parcService.GetParcByIdAsync((int)id);
            if (parc == null)
            {
                return NotFound();
            }

            return Ok(parc);
        }


        // POST: Parcs/Add
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Add([Bind("Name,IsActive")] Parc parc)
        {
            if (ModelState.IsValid)
            {
                await _parcService.CreateParcAsync(parc);
                return Ok(parc);
            }
            return Ok(parc);
        }

        // POST: Parcs/Update/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [Bind("Name,IsActive")] Parc parc)
        {
            if (ModelState.IsValid)
            {
                var parcUpdate = new Parc();
                try
                {
                    parcUpdate = await _parcService.UpdateParcAsync(id,parc); 
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParcExists(parc.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok(parcUpdate);
            }
            return Ok(parc);
        }

        // POST: Parcs/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _parcService.DeleteParcAsync(id);
            }
            catch {
                return NotFound();
            }
            return Ok(true);
        }

        private bool ParcExists(int id)
        {
            return _context.Parc.Any(e => e.Id == id);
        }
    }
}
