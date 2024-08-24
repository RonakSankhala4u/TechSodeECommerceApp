using System.ComponentModel.DataAnnotations;

namespace TechSodeECommerceApp.ViewModels
{
    public class RoleViewModel
    {
        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}
