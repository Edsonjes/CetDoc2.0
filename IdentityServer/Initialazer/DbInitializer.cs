using IdentityModel;
using IdentityService.Configurations;
using IdentityService.Initialazer;
using IdentityService.Models;
using IdentityService.Models.Context;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;


namespace IdentityService.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly DBConections _dbConnection;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(DBConections dbConnection, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dbConnection = dbConnection;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Initialize()
        {
            if (await _roleManager.FindByNameAsync(IdentityConfiguration.Admin) != null) return;

            await _roleManager.CreateAsync(new IdentityRole(IdentityConfiguration.Admin));
            await _roleManager.CreateAsync(new IdentityRole(IdentityConfiguration.Client));

            var admin = new ApplicationUser
            {
                UserName = "Edson-admin",
                Email = "edsonjaj@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "+55 (61) 992510901",
                FirstName = "Edson",
                LastName = "Admin",
            };

            await _userManager.CreateAsync(admin, "Erudio123$");
            await _userManager.AddToRoleAsync(admin, IdentityConfiguration.Admin);

            var adminClaims = await _userManager.AddClaimsAsync(admin, new Claim[]{
                new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin),
            });

            var client = new ApplicationUser
            {
                UserName = "Edson-Client",
                Email = "edsonjaj@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "+55 (61) 992510901",
                FirstName = "Edson",
                LastName = "Client",
            };

            await _userManager.CreateAsync(client, "Erudio123$");
            await _userManager.AddToRoleAsync(client, IdentityConfiguration.Client);

            var clientClaims = await _userManager.AddClaimsAsync(client, new Claim[]{
                new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                new Claim(JwtClaimTypes.GivenName, client.FirstName),
                new Claim(JwtClaimTypes.FamilyName, client.LastName),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client),
            });
        }
    }
}
