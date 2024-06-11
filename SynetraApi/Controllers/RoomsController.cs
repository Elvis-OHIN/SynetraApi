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
    [Authorize(Roles = "SuperAdmin,Admin")]
    [Route("api/[controller]")]
    public class RoomsController : Controller
    {
        private readonly DataContext _context;
        private readonly IRoomService _roomService;

        public RoomsController(DataContext context, IRoomService roomService)
        {
            _context = context;
            _roomService = roomService;
        }

        /// <summary>
        /// Récupère la liste de toutes les salles.
        /// </summary>
        /// <returns>Retourne une liste des salles.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _roomService.GetRoomsAsync());
        }

        /// <summary>
        /// Récupère la liste des salles par identifiant de parc.
        /// </summary>
        /// <param name="id">L'identifiant du parc.</param>
        /// <returns>Retourne une liste des salles pour un parc spécifique.</returns>
        [HttpGet("Parc/{id}")]
        public async Task<IActionResult> GetRoomByParc(int id)
        {
            try
            {
                return Ok(await _roomService.GetRoomsByParcAsync(id));
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Récupère les détails d'une salle spécifique.
        /// </summary>
        /// <param name="id">L'identifiant de la salle.</param>
        /// <returns>Retourne les détails de la salle.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            var room = await _roomService.GetRoomByIdAsync((int)id);
            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        /// <summary>
        /// Crée une nouvelle salle.
        /// </summary>
        /// <param name="rooms">Les détails de la salle à créer.</param>
        /// <returns>Retourne la salle créée si le modèle est valide.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,RoomsId,IsActive,IsEnable,CreatedDate,UpdatedDate")] Room rooms)
        {
            if (ModelState.IsValid)
            {
                await _roomService.CreateRoomAsync(rooms);
                return Ok(rooms);
            }
            return Ok(rooms);
        }

        /// <summary>
        /// Met à jour une salle existante.
        /// </summary>
        /// <param name="id">L'identifiant de la salle à mettre à jour.</param>
        /// <param name="rooms">Les détails mis à jour de la salle.</param>
        /// <returns>Retourne la salle mise à jour si le modèle est valide.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,RoomsId,IsActive,IsEnable,CreatedDate,UpdatedDate")] Room rooms)
        {
            if (ModelState.IsValid)
            {
                var RoomUpdate = new Room();
                try
                {
                    RoomUpdate = await _roomService.UpdateRoomAsync(id, rooms);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomsExists(rooms.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok(RoomUpdate);
            }
            return Ok(rooms);
        }

        /// <summary>
        /// Supprime une salle spécifique.
        /// </summary>
        /// <param name="id">L'identifiant de la salle à supprimer.</param>
        /// <returns>Retourne OK avec true si la suppression réussit, sinon NotFound.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _roomService.DeleteRoomAsync(id);
            }
            catch
            {
                return NotFound();
            }
            return Ok(true);
        }


        private bool RoomsExists(int id)
        {
            return _context.Room.Any(e => e.Id == id);
        }
    }
}
