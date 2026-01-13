using System.ComponentModel.DataAnnotations;

namespace AdminDashboard.ViewModels.UserViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; } = string.Empty;

        [Display(Name = "Display Name")]
        public string DisplayName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public IEnumerable<string> Roles { get; set; } = [];    
    }
}
