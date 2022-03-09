using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager) 
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "John Smith",
                    Email = "johmSmith@test.com",
                    UserName = "JohnSmith",
                    Address = new Address
                    {
                        FirstName = "John",
                        LastName = "Smith",
                        City = "NY",
                        State = "BD",
                        ZipCode = "43212",
                    }
                };

                await userManager.CreateAsync(user, "John1234!");
            }
        }
    }
}
