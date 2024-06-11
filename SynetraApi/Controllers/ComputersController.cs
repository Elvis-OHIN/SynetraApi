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


        /// <summary>
        /// R�cup�re une liste d'ordinateurs.
        /// </summary>
        /// <returns>Retourne une liste d'ordinateurs en tant qu'op�ration asynchrone.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _computerService.GetComputersAsync());
        }

        /// <summary>
        /// R�cup�re une liste d'ordinateurs par parc.
        /// </summary>
        /// <param name="id">L'identifiant du parc.</param>
        /// <returns>Retourne une liste d'ordinateurs en tant qu'op�ration asynchrone.</returns>
        [HttpGet("Parc/{id}")]
        public async Task<IActionResult> GetComputerByParc(int id)
        {
            try
            {
                return Ok(await _computerService.GetComputersByParcAsync(id));
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// R�cup�re les d�tails d'un ordinateur sp�cifique.
        /// </summary>
        /// <param name="id">L'identifiant de l'ordinateur.</param>
        /// <returns>Retourne les d�tails de l'ordinateur en tant qu'op�ration asynchrone.</returns>
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




        /// <summary>
        /// Cr�e un nouvel ordinateur.
        /// </summary>
        /// <param name="computers">Les d�tails de l'ordinateur � cr�er.</param>
        /// <returns>Retourne l'ordinateur cr�� si le mod�le est valide.</returns>
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

        /// <summary>
        /// Met � jour les d�tails d'un ordinateur existant.
        /// </summary>
        /// <param name="id">L'identifiant de l'ordinateur � mettre � jour.</param>
        /// <param name="computers">Les d�tails mis � jour de l'ordinateur.</param>
        /// <returns>Retourne l'ordinateur mis � jour si le mod�le est valide.</returns>
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
        /// <summary>
        /// Met � jour l'empreinte d'un ordinateur.
        /// </summary>
        /// <param name="id">L'identifiant de l'ordinateur � mettre � jour.</param>
        /// <param name="footPrint">La nouvelle empreinte de l'ordinateur.</param>
        /// <returns>Retourne OK si l'op�ration r�ussit.</returns>
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

        /// <summary>
        /// V�rifie l'existence d'un ordinateur par empreinte.
        /// </summary>
        /// <param name="footPrint">L'empreinte de l'ordinateur.</param>
        /// <returns>Retourne l'ordinateur s'il existe, sinon NotFound.</returns>
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

        /// <summary>
        /// R�cup�re la connexion d'un ordinateur par son identifiant.
        /// </summary>
        /// <param name="id">L'identifiant de l'ordinateur.</param>
        /// <returns>Retourne la connexion de l'ordinateur s'il existe, sinon NotFound.</returns>
        [HttpGet("Connection/{id}")]
        public async Task<IActionResult> ComputersConnectionById(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computers = await _computerService.GetComputerConnectionAsync(id);
            if (computers == null)
            {
                return NotFound();
            }

            return Ok(computers);
        }


        /// <summary>
        /// Cr�e une nouvelle connexion pour un ordinateur.
        /// </summary>
        /// <param name="id">L'identifiant de l'ordinateur.</param>
        /// <param name="connection">Les d�tails de la connexion � cr�er.</param>
        /// <returns>Retourne l'ordinateur mis � jour avec la nouvelle connexion, sinon NotFound.</returns>
        [HttpPut("Connection/{id}")]
        public async Task<IActionResult> CreateConnection(int id, Connection connection)
        {
            if (ModelState.IsValid)
            {
                Computer computerUpdate = new Computer();
                try
                {
                    computerUpdate = await _computerService.CreateComputerConnectionAsync(id, connection);
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
                return Ok(computerUpdate);
            }
            return NotFound();
        }


        /// <summary>
        /// Supprime un ordinateur sp�cifique.
        /// </summary>
        /// <param name="id">L'identifiant de l'ordinateur � supprimer.</param>
        /// <returns>Retourne OK avec true si la suppression r�ussit, sinon NotFound.</returns>
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
