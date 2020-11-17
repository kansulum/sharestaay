using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentitySeed
    {
          public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    
                    Email = "bob@test.com",
                    UserName = "bob@test.com",
                    
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}