using AdminDashboard.ViewModels.RoleViewModels;
using System.ComponentModel.DataAnnotations;

namespace AdminDashboard.ViewModels.UserViewModels
{
    public class UserRoleViewModel
    {
        [Display(Name = "User ID")]
        public string UserId { get; set; } = string.Empty;
        [Display(Name = "User Name")]
        public string UserName { get; set; } = string.Empty;
        public List<UpdateRoleViewModel> Roles { get; set; } = [];
    }
}
