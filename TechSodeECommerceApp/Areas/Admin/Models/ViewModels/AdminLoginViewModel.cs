using System.ComponentModel.DataAnnotations;

namespace TechSodeECommerceApp.Areas.Admin.Models.ViewModels
{
    public class AdminLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
