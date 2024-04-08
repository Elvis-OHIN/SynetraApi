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
    public class ComputersController : Controller
    {
        private readonly DataContext _context;
        private readonly IComputerService _computerService;

        public ComputersController(DataContext context, IComputerService computerService)
        {
            _context = context;
            _computerService = computerService;
        }

        // GET: Computers
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _computerService.GetComputersAsync());
        }

        // GET: Computers/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computers = await _computerService.GetComputerByIdAsync((int)id);
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
        public async Task<IActionResult> Create([Bind("Id,Name,IDProduct,OperatingSystem,Os,CarteMere,GPU,RoomId,Statut,IsActive")] Computer computers)
        {
            if (ModelState.IsValid)
            {
                await _computerService.CreateComputerAsync(computers);
                return Ok(computers);
            }
            return Ok(computers);
        }

        // POST: Computers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IDProduct,OperatingSystem,Os,CarteMere,GPU,RoomId,Statut,IsActive,IsEnable,CreatedDate,UpdatedDate")] Computer computers)
        {
            if (ModelState.IsValid)
            {
                Computer computerUpdate = new Computer();
                try
                {
                    computerUpdate = await _computerService.UpdateComputerAsync(id, computers);
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
                return Ok(computerUpdate);
            }
            return Ok(computers);
        }

        [HttpPut("FootPrint/{id}")]
        public async Task<IActionResult> EditFootPrint(int id, string footPrint)
        {
            if (ModelState.IsValid)
            {
                Computer computerUpdate = new Computer();
                try
                {
                    computerUpdate = await _computerService.UpdateComputerFootPrintAsync(id, footPrint);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComputersExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok();
            }
            return Ok();
        }

        [HttpGet("FootPrint/{footPrint}")]
        public async Task<IActionResult> ComputersExistsByFootPrint(string? footPrint)
        {
            if (footPrint == null)
            {
                return NotFound();
            }

            var computers = await _computerService.GetComputerByFootPrintAsync(footPrint);
            if (computers == null)
            {
                return NotFound();
            }

            return Ok(computers);
        }



            // POST: Computers/Delete/5
            [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _computerService.DeleteComputerAsync(id);
            }
            catch
            {
                return NotFound();
            }
            return Ok(true);
        }

        private bool ComputersExists(int id)
        {
            return _context.Computer.Any(e => e.Id == id);
        }
    }
}
