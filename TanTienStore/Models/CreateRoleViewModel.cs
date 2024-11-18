using System.ComponentModel.DataAnnotations;

namespace TanTienStore.Models
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}
