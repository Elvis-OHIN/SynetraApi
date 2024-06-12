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

               SeedDB(context);
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

            context.Room.AddRange(
                new Room { Name = "Salle 101", ParcId = 1, IsActive = true, IsEnable = true , CreatedDate = DateTime.Now },
                new Room { Name = "Salle 102", ParcId = 1, IsActive = true, IsEnable = true , CreatedDate = DateTime.Now },
                new Room { Name = "Salle 201", ParcId = 2, IsActive = true, IsEnable = true, CreatedDate = DateTime.Now },
                new Room { Name = "Salle 202", ParcId = 2, IsActive = true, IsEnable = true ,CreatedDate = DateTime.Now },
                new Room { Name = "Salle 301", ParcId = 3, IsActive = true, IsEnable = true, CreatedDate = DateTime.Now },
                new Room { Name = "Salle 302", ParcId = 3, IsActive = true, IsEnable = true, CreatedDate = DateTime.Now }
            );
            context.Computer.AddRange(
                new Computer { Name = "Ordinateur A1", IDProduct = "12345", OperatingSystem = "Windows 10", Os = "Windows", CarteMere = "CarteMere A1", GPU = "GPU A1", IsActive = true, IsEnable = true, CreatedDate = DateTime.Now, RoomId = 1 },
                new Computer { Name = "Ordinateur A2", IDProduct = "12346", OperatingSystem = "Windows 10", Os = "Windows", CarteMere = "CarteMere A2", GPU = "GPU A2", IsActive = true, IsEnable = true, CreatedDate = DateTime.Now, RoomId = 1 },
                new Computer { Name = "Ordinateur B1", IDProduct = "22345", OperatingSystem = "Ubuntu 20.04", Os = "Linux", CarteMere = "CarteMere B1", GPU = "GPU B1", IsActive = true, IsEnable = true, CreatedDate = DateTime.Now, RoomId = 3 },
                new Computer { Name = "Ordinateur B2", IDProduct = "22346", OperatingSystem = "Ubuntu 20.04", Os = "Linux", CarteMere = "CarteMere B2", GPU = "GPU B2", IsActive = true, IsEnable = true, CreatedDate = DateTime.Now, RoomId = 3 },
                new Computer { Name = "Ordinateur C1", IDProduct = "32345", OperatingSystem = "macOS Big Sur", Os = "Mac", CarteMere = "CarteMere C1", GPU = "GPU C1", IsActive = true, IsEnable = true, CreatedDate = DateTime.Now,  RoomId = 5 },
                new Computer { Name = "Ordinateur C2", IDProduct = "32346", OperatingSystem = "macOS Big Sur", Os = "Mac", CarteMere = "CarteMere C2", GPU = "GPU C2", IsActive = true, IsEnable = true, CreatedDate = DateTime.Now, RoomId = 5 }
          );
            context.SaveChanges();
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
                    EmailConfirmed = true,
                    IsActive = true,
                    IsEnable = true,
                    CreatedDate = DateTime.Now, 

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
