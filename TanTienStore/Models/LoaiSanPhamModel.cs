using System.ComponentModel.DataAnnotations;

namespace TanTienStore.Models
{
    public class LoaiSanPhamModel
    {
        [Key]
        public int LoaiSPId { get; set; }
        public string Name { get; set; }
    }
}
