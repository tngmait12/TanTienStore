using System.ComponentModel.DataAnnotations;

namespace TanTienStore.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Full Name is Required")]
        [Display(Name = "Full Name")]
        public string Fullname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
