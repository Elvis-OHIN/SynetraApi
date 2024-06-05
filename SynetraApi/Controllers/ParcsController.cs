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
        /// Récupère la liste de tous les parcs.
        /// </summary>
        /// <returns>Retourne une liste des parcs.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllParcs()
        {
            return Ok(await _parcService.GetParcsAsync());
        }

        /// <summary>
        /// Récupère les détails d'un parc spécifique.
        /// </summary>
        /// <param name="id">L'identifiant du parc.</param>
        /// <returns>Retourne les détails du parc.</returns>
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
        /// <param name="parc">Les détails du parc à ajouter.</param>
        /// <returns>Retourne le parc ajouté si le modèle est valide.</returns>
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
        /// Met à jour un parc existant.
        /// </summary>
        /// <param name="id">L'identifiant du parc à mettre à jour.</param>
        /// <param name="parc">Les détails mis à jour du parc.</param>
        /// <returns>Retourne le parc mis à jour si le modèle est valide.</returns>
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
        /// Supprime un parc spécifique.
        /// </summary>
        /// <param name="id">L'identifiant du parc à supprimer.</param>
        /// <returns>Retourne OK avec true si la suppression réussit, sinon NotFound.</returns>
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
