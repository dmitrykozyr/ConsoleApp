using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SecuringRestApiAspNetCore.DatabaseContext;
using SecuringRestApiAspNetCore.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringRestApiAspNetCore
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            // Authentication and authorization
            await AddTestUser(services.GetRequiredService<RoleManager<UserRoleEntity>>(),
                              services.GetRequiredService<UserManager<UserEntity>>());

            await AddTestData(services.GetRequiredService<HotelApiDbContext>());
        }

        public static async Task AddTestData(HotelApiDbContext context)
        {
            if (context.Rooms.Any())
            {
                // Already have data
                return;
            }

            var tmp1 = Guid.NewGuid();
            var tmp2 = Guid.NewGuid();

            context.Rooms.Add(new RoomEntity
            {
                Id = new Guid("19e49087-1dc7-46df-a5a1-25db8c4d86f1"),
                Name = "Oxford Suite",
                Rate = 10119
            });

            context.Rooms.Add(new RoomEntity
            {
                Id = new Guid("861d65e1-65d6-4524-8f48-55105e5aaa25"),
                Name = "Driscoll Suite",
                Rate = 23959
            });

            await context.SaveChangesAsync();
        }

        private static async Task AddTestUser(
                                    RoleManager<UserRoleEntity> roleManager,
                                    UserManager<UserEntity> userManager)
        {
            var isDataExists = roleManager.Roles.Any() || userManager.Users.Any();
            if (isDataExists)
                return;

            // Add test role
            await roleManager.CreateAsync(new UserRoleEntity("Admin"));

            // Add test user
            var user = new UserEntity()
            {
                Email = "user1@email.com",
                UserName = "user1",
                FirstName = "Admin",
                LastName = "Tester",
                CreatedAt = DateTimeOffset.UtcNow
            };

            await userManager.CreateAsync(user, "PassWord123");

            // Put user into Admin role
            await userManager.AddToRoleAsync(user, "Admin");
            await userManager.UpdateAsync(user);
        }
    }
}
