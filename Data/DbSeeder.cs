using Microsoft.AspNetCore.Identity;

namespace El_Mooo_Clinic.Data 
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = service.GetRequiredService<UserManager<IdentityUser>>();

            string[] roles = { "Receptionist", "Doctor" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var receptionistEmail = "Receptionist1@elmooo.com";
            if (await userManager.FindByEmailAsync(receptionistEmail) == null)
            {
                var user = new IdentityUser
                {
                    UserName = receptionistEmail,
                    Email = receptionistEmail,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, "Receptionist1@123"); 
                await userManager.AddToRoleAsync(user, "Receptionist");
            }

            var doctorEmail = "Moamenazm@elmooo.com";
            if (await userManager.FindByEmailAsync(doctorEmail) == null)
            {
                var user = new IdentityUser
                {
                    UserName = doctorEmail,
                    Email = doctorEmail,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, "Moamenazm@123"); 
                await userManager.AddToRoleAsync(user, "Doctor");
            }
        }
    }
}