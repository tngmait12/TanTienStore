using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TanTienStore.Models
{
    public class KhachHangModel
    {
        [Key]
        public int MaKH { get; set; }
        [Required]
        [MaxLength(20)]
        public string Ho { get; set; } = string.Empty;
        [Required]
        [MaxLength(30)]
        public string TenDem { get; set; } = string.Empty;
        [Required]
        [MaxLength(20)]
        public string Ten { get; set; } = string.Empty;
        public bool GioiTinh { get; set; } = true;
        [Required]
        [MaxLength(255)]
        public string? DiaChi { get; set; }
        [Required]
        [MaxLength(20)]
        public string? SDT { get; set; }
        public int DiemTichLuy { get; set; } = 0;
        
    }
}
