using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SynetraApi.Authorization;
using SynetraUtils.Models.DataManagement;

namespace SynetraApi.Data
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw, string superAdmin , string admin, string testPassword , string testAdmin)
        {
            using (var context = new DataContext(
                serviceProvider.GetRequiredService<DbContextOptions<DataContext>>()))
            {
                
                var superAdminID = await EnsureUser(serviceProvider, testUserPw, superAdmin,0);
                await EnsureRole(serviceProvider, superAdminID, Constants.UserSuperAdminRole);

                SeedDB(context);

                var adminID = await EnsureUser(serviceProvider, testUserPw, admin, 1);
                await EnsureRole(serviceProvider, adminID, Constants.UserAdminRole);

                var adminTestId = await EnsureUser(serviceProvider, testPassword, testAdmin, 2);
                await EnsureRole(serviceProvider, adminTestId, Constants.UserAdminRole);
            }
        }
        public static void SeedDB(DataContext context)
        {
            if (context.Parc.Any())
            {
                return;  
            }

            context.Parc.AddRange(
                new Parc { Name = "3iL Paris", IsActive = true, IsEnable = true , CreatedDate = DateTime.Now },
                new Parc { Name = "3iL Lyon", IsActive = true, IsEnable = true, CreatedDate = DateTime.Now },
                new Parc { Name = "3iL Limoges", IsActive = true, IsEnable = true, CreatedDate = DateTime.Now }
             );

            context.SaveChanges();

            context.Room.AddRange(
                new Room { Name = "Salle 101", ParcId = 1, IsActive = true, IsEnable = true , CreatedDate = DateTime.Now },
                new Room { Name = "Salle 102", ParcId = 1, IsActive = true, IsEnable = true , CreatedDate = DateTime.Now },
                new Room { Name = "Salle 201", ParcId = 2, IsActive = true, IsEnable = true, CreatedDate = DateTime.Now },
                new Room { Name = "Salle 202", ParcId = 2, IsActive = true, IsEnable = true ,CreatedDate = DateTime.Now },
                new Room { Name = "Salle 301", ParcId = 3, IsActive = true, IsEnable = true, CreatedDate = DateTime.Now },
                new Room { Name = "Salle 302", ParcId = 3, IsActive = true, IsEnable = true, CreatedDate = DateTime.Now }
            );

            context.SaveChanges();

            context.Computer.AddRange(
                new Computer { Name = "Ordinateur A1", IDProduct = "12345", OperatingSystem = "Windows 10", Os = "Windows", CarteMere = "CarteMere A1", GPU = "GPU A1", IsActive = true, IsEnable = true, CreatedDate = DateTime.Now, RoomId = 1 , ParcId = 1 },
                new Computer { Name = "Ordinateur A2", IDProduct = "12346", OperatingSystem = "Windows 10", Os = "Windows", CarteMere = "CarteMere A2", GPU = "GPU A2", IsActive = true, IsEnable = true, CreatedDate = DateTime.Now, RoomId = 1 , ParcId = 1 },
                new Computer { Name = "Ordinateur B1", IDProduct = "22345", OperatingSystem = "Ubuntu 20.04", Os = "Linux", CarteMere = "CarteMere B1", GPU = "GPU B1", IsActive = true, IsEnable = true, CreatedDate = DateTime.Now, RoomId = 3 , ParcId = 2 },
                new Computer { Name = "Ordinateur B2", IDProduct = "22346", OperatingSystem = "Ubuntu 20.04", Os = "Linux", CarteMere = "CarteMere B2", GPU = "GPU B2", IsActive = true, IsEnable = true, CreatedDate = DateTime.Now, RoomId = 3, ParcId = 2 },
                new Computer { Name = "Ordinateur C1", IDProduct = "32345", OperatingSystem = "macOS Big Sur", Os = "Mac", CarteMere = "CarteMere C1", GPU = "GPU C1", IsActive = true, IsEnable = true, CreatedDate = DateTime.Now,  RoomId = 5, ParcId = 3 },
                new Computer { Name = "Ordinateur C2", IDProduct = "32346", OperatingSystem = "macOS Big Sur", Os = "Mac", CarteMere = "CarteMere C2", GPU = "GPU C2", IsActive = true, IsEnable = true, CreatedDate = DateTime.Now, RoomId = 5, ParcId = 3 }
          );
            context.SaveChanges();
        }

        private static async Task<int> EnsureUser(IServiceProvider serviceProvider,
                                                    string testUserPw, string UserName, int parcId)
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
                    Email = UserName,
                    EmailConfirmed = true,
                    IsActive = true,
                    IsEnable = true,
                    CreatedDate = DateTime.Now, 

                };
                if (parcId > 0)
                {
                    user.ParcId = parcId;
                }
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
