using Microsoft.AspNetCore.Mvc.Rendering;

namespace TechSodeECommerceApp.ViewModels
{
    public class RoleMappingViewModel
    {
        public string SelectedUserId { get; set; }
        public string SelectedRoleId { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
