using Microsoft.AspNetCore.Identity;

namespace TechSodeECommerceApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsActive { get; set; } = true; // Default value set to true
    }
}
