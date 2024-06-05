using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SynetraApi.Data;
using SynetraApi.Models;
using SynetraUtils.Models.DataManagement;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SynetraApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "SuperAdmin")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly DataContext _context;

        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager, DataContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context;
        }


        /// <summary>
        /// Enregistre un nouvel utilisateur.
        /// </summary>
        /// <param name="user">Les détails de l'utilisateur à enregistrer.</param>
        /// <returns>Retourne l'identifiant de l'utilisateur enregistré si l'enregistrement réussit, sinon une erreur.</returns>
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            user.CreatedDate = DateTime.Now;
            user.IsEnable = true;
            var result = await userManager.CreateAsync(user, user.PasswordHash!);

            if (result.Succeeded)
            {
                var id = await userManager.GetUserIdAsync(user);
                return Ok(Convert.ToInt32(id));
            }

            return BadRequest("Error occurred");
        }

        /// <summary>
        /// Met à jour un utilisateur existant.
        /// </summary>
        /// <param name="id">L'identifiant de l'utilisateur à mettre à jour.</param>
        /// <param name="user">Les détails mis à jour de l'utilisateur.</param>
        /// <returns>Retourne OK si la mise à jour réussit, sinon une erreur.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User user)
        {
            User userUpdate = await userManager.FindByIdAsync($"{id}");

            if (userUpdate != null)
            {
                userUpdate.Email = user.Email;
                userUpdate.Firstname = user.Firstname;
                userUpdate.Lastname = user.Lastname;
                userUpdate.UserName = user.UserName;
                userUpdate.IsActive = user.IsActive;
                userUpdate.UpdatedDate = DateTime.Now;

                var result = await userManager.UpdateAsync(userUpdate);

                if (user.PasswordHash is not null)
                {
                    await userManager.ChangePasswordAsync(userUpdate, userUpdate.PasswordHash, user.PasswordHash);
                }

                if (result.Succeeded)
                {
                    return Ok();
                }
            }
            return BadRequest("Error occurred");
        }

        /// <summary>
        /// Désactive un utilisateur spécifique.
        /// </summary>
        /// <param name="id">L'identifiant de l'utilisateur à désactiver.</param>
        /// <returns>Retourne OK si la désactivation réussit, sinon une erreur.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            User userUpdate = await userManager.FindByIdAsync($"{id}");

            if (userUpdate != null)
            {
                userUpdate.IsEnable = false;
                userUpdate.UpdatedDate = DateTime.Now;
                var result = await userManager.UpdateAsync(userUpdate);

                if (result.Succeeded)
                {
                    return Ok();
                }
            }
            return BadRequest("Error occurred");
        }

        /// <summary>
        /// Récupère la liste de tous les utilisateurs actifs.
        /// </summary>
        /// <returns>Retourne une liste des utilisateurs actifs.</returns>
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(userManager.Users.Where(u => u.IsEnable == true).ToList());
        }

        /// <summary>
        /// Récupère la liste des rôles des utilisateurs.
        /// </summary>
        /// <returns>Retourne une liste des rôles des utilisateurs.</returns>
        [HttpGet("Role")]
        public async Task<IActionResult> GetUsersRole()
        {
            return Ok(_context.UserRoles.ToList());
        }

        /// <summary>
        /// Récupère la liste des utilisateurs sauf l'utilisateur actuel.
        /// </summary>
        /// <param name="email">L'adresse e-mail de l'utilisateur actuel.</param>
        /// <returns>Retourne une liste des utilisateurs sauf l'utilisateur actuel.</returns>
        [HttpGet("Except/{email}")]
        public async Task<IActionResult> GetUsersExceptCurrentUser(string email)
        {
            return Ok(userManager.Users.Where(r => r.Email != email && r.IsEnable == true));
        }

    }
}
