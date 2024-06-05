using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "SuperAdmin")]
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

        /// <summary>
        /// R�cup�re la liste de tous les parcs.
        /// </summary>
        /// <returns>Retourne une liste des parcs.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllParcs()
        {
            return Ok(await _parcService.GetParcsAsync());
        }

        /// <summary>
        /// R�cup�re les d�tails d'un parc sp�cifique.
        /// </summary>
        /// <param name="id">L'identifiant du parc.</param>
        /// <returns>Retourne les d�tails du parc.</returns>
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

        /// <summary>
        /// Ajoute un nouveau parc.
        /// </summary>
        /// <param name="parc">Les d�tails du parc � ajouter.</param>
        /// <returns>Retourne le parc ajout� si le mod�le est valide.</returns>
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

        /// <summary>
        /// Met � jour un parc existant.
        /// </summary>
        /// <param name="id">L'identifiant du parc � mettre � jour.</param>
        /// <param name="parc">Les d�tails mis � jour du parc.</param>
        /// <returns>Retourne le parc mis � jour si le mod�le est valide.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [Bind("Name,IsActive")] Parc parc)
        {
            if (ModelState.IsValid)
            {
                var parcUpdate = new Parc();
                try
                {
                    parcUpdate = await _parcService.UpdateParcAsync(id, parc);
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

        /// <summary>
        /// Supprime un parc sp�cifique.
        /// </summary>
        /// <param name="id">L'identifiant du parc � supprimer.</param>
        /// <returns>Retourne OK avec true si la suppression r�ussit, sinon NotFound.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _parcService.DeleteParcAsync(id);
            }
            catch
            {
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
