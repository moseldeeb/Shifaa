using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Shifaa.Utilities
{
    public class DBInitializr : IDBInitializr
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DBInitializr> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public DBInitializr(
            ApplicationDbContext context,
            ILogger<DBInitializr> logger,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager
            )
        {
            _context = context;
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void Initialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Any())
                {
                    _context.Database.Migrate();
                }
                if (!_roleManager.Roles.Any())
                {
                    _roleManager.CreateAsync(new(SD.SUPER_ADMIN_ROLE)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new(SD.ADMIN_ROLE)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new(SD.DOCTOR_ROLE)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new(SD.MEMBER_ROLE)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new(SD.CARE_GIVER_ROLE)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new(SD.MEDICAL_CENTER_ROLE)).GetAwaiter().GetResult();

                    _userManager.CreateAsync(new ApplicationUser()
                    {
                        Email = "SuperAdmin@Shifaa.com",
                        UserName = "SuperAdmin@Shifaa.com",
                        FirstName = "Super",
                        LastName = "Admin",
                        EmailConfirmed = true,
                        UserType = Enums.UserType.SuperAdmin,     // ← add this
                        CreatedAt = DateTime.UtcNow,
                        IsDeleted = false
                    }, "Admin@123").GetAwaiter().GetResult();
                    var user = _userManager.FindByNameAsync("SuperAdmin").GetAwaiter().GetResult();
                    _userManager.AddToRoleAsync(user, SD.SUPER_ADMIN_ROLE).GetAwaiter().GetResult();

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

        }
    }
}
