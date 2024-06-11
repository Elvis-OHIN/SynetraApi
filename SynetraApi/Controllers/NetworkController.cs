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
    [Authorize(Roles = "SuperAdmin,Admin")]
    [Route("api/[controller]")]
    public class NetworkController : Controller
    {
        private readonly DataContext _context;

        public NetworkController(DataContext context)
        {
            _context = context;
        }

        // GET: Network
        /// <summary>
        /// Récupère la liste de toutes les informations réseau.
        /// </summary>
        /// <returns>Retourne une liste des informations réseau.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.NetworkInfo.Include(n => n.Computer);
            return Ok(await dataContext.ToListAsync());
        }

        /// <summary>
        /// Récupère les détails d'une information réseau spécifique.
        /// </summary>
        /// <param name="id">L'identifiant de l'information réseau.</param>
        /// <returns>Retourne les détails de l'information réseau.</returns>
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

        /// <summary>
        /// Crée une nouvelle information réseau.
        /// </summary>
        /// <param name="networkInfo">Les détails de l'information réseau à créer.</param>
        /// <returns>Redirige vers l'index si la création réussit, sinon retourne l'information réseau avec les erreurs de modèle.</returns>
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

        /// <summary>
        /// Modifie une information réseau existante.
        /// </summary>
        /// <param name="id">L'identifiant de l'information réseau à modifier.</param>
        /// <param name="networkInfo">Les détails mis à jour de l'information réseau.</param>
        /// <returns>Redirige vers l'index si la modification réussit, sinon retourne l'information réseau avec les erreurs de modèle.</returns>
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

        /// <summary>
        /// Supprime une information réseau spécifique.
        /// </summary>
        /// <param name="id">L'identifiant de l'information réseau à supprimer.</param>
        /// <returns>Redirige vers l'index après la suppression.</returns>
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
