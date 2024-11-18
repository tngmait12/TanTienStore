using System.ComponentModel.DataAnnotations;

namespace TanTienStore.Models
{
    public class EditUserViewModel
    {
        //To avoid NullReferenceExceptions at runtime,
        //initialise Claims and Roles with a new list in the constructor.
        public EditUserViewModel()
        {
            Roles = new List<string>();
        }

        [Required]
        public string Id { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Full Name")]
        public string? Fullname { get; set; }
        [Display(Name = "Phone Number")]
        public string? DienThoai { get; set; }

        public IList<string> Roles { get; set; }
    }
}