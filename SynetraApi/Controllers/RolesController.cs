using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SynetraApi.Data;
using SynetraUtils.Models.DataManagement;

namespace SynetraApi.Controllers
{
    [ApiController]
    [Authorize(Roles = "SuperAdmin")]
    [Route("api/[controller]")]
    public class RolesController : Controller
    {
        private readonly DataContext _context;
        public RolesController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupère la liste de tous les rôles.
        /// </summary>
        /// <returns>Retourne une liste des rôles.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<IdentityRole<int>> roles = _context.Roles.ToList();
            return Ok(roles);
        }

        /// <summary>
        /// Assigne un rôle à un utilisateur.
        /// </summary>
        /// <param name="userRole">Les détails de l'affectation du rôle à l'utilisateur.</param>
        /// <returns>Retourne OK si l'affectation réussit, sinon une erreur.</returns>
        [HttpPost("User")]
        public async Task<IActionResult> Create(IdentityUserRole<int> userRole)
        {
            try
            {
                _context.UserRoles.Add(userRole);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
