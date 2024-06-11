using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SynetraApi.Authorization;
using SynetraUtils.Models.DataManagement;

namespace SynetraApi.Data
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = new DataContext(
                serviceProvider.GetRequiredService<DbContextOptions<DataContext>>()))
            {
                // For sample purposes seed both with the same password.
                // Password is set with the following:
                // dotnet user-secrets set SeedUserPW <pw>
                // The admin user can do anything

                
                var superAdminID = await EnsureUser(serviceProvider, testUserPw, "superadmin@synetra.com");
                await EnsureRole(serviceProvider, superAdminID, Constants.UserSuperAdminRole);

                // allowed user can create and edit contacts that they create
                var adminID = await EnsureUser(serviceProvider, testUserPw, "admin@3il.com");
                await EnsureRole(serviceProvider, adminID, Constants.UserAdminRole);

                //SeedDB(context, adminID);
            }
        }

        private static async Task<int> EnsureUser(IServiceProvider serviceProvider,
                                                    string testUserPw, string UserName)
        {

            var userManager = serviceProvider.GetService<UserManager<User>>();

            if (userManager == null)
            {
                throw new Exception("userManager null");
            }

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new User
                {
                    UserName = UserName,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      int uid, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole<int>>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            IdentityResult IR;
            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole<int>(role));
            }

            var userManager = serviceProvider.GetService<UserManager<User>>();

            //if (userManager == null)
            //{
            //    throw new Exception("userManager is null");
            //}

            var user = await userManager.FindByIdAsync($"{uid}");

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
    }
}
