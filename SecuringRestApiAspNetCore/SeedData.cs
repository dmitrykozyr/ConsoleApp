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
    }
}
