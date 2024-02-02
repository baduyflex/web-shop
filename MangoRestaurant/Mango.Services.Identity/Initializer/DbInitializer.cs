using Duende.IdentityServer.Validation;
using IdentityModel;
using Mango.Services.Identity.DbContexts;
using Mango.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Mango.Services.Identity.Initializer
{   
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer() { }
        public DbInitializer(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {   
            if(_roleManager.FindByNameAsync(SD.Admin).Result == null)
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Customer)).GetAwaiter().GetResult();
            }
            else
            {
                return;
            }

            #region AdminUser
            ApplicationUser adminUser = new ApplicationUser()
            {
                UserName = "admin1@gmail.com",
                Email = "admin1@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "0939891927",
                FirstName = "Ben",
                LastName = "Admin",
            };

            _userManager.CreateAsync(adminUser, "Admin123*").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(adminUser, SD.Admin).GetAwaiter().GetResult();

            var temp1 = _userManager.AddClaimsAsync(adminUser, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, adminUser.FirstName + " " + adminUser.LastName),
                new Claim(JwtClaimTypes.GivenName, adminUser.FirstName),
                new Claim(JwtClaimTypes.FamilyName, adminUser.LastName),
                new Claim(JwtClaimTypes.Role, SD.Admin),
            }).Result;
            #endregion

            #region
            ApplicationUser customer = new ApplicationUser()
            {
                UserName = "customer1@gmail.com",
                Email = "customer1@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "0939891927",
                FirstName = "Ben",
                LastName = "Customer",
            };

            _userManager.CreateAsync(customer, "Admin123*").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(customer, SD.Admin).GetAwaiter().GetResult();

            var temp2 = _userManager.AddClaimsAsync(customer, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, customer.FirstName + " " + customer.LastName),
                new Claim(JwtClaimTypes.GivenName, customer.FirstName),
                new Claim(JwtClaimTypes.FamilyName, adminUser.LastName),
                new Claim(JwtClaimTypes.Role, SD.Customer),
            }).Result;
            #endregion

        }
    }
}
