using Microsoft.AspNetCore.Identity;

namespace TanTienStore.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Fullname { get; set; }
    }
}
