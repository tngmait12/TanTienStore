using System.ComponentModel.DataAnnotations;

namespace TanTienStore.Models
{
    public class LoaiSanPhamModel
    {
        [Key]
        public int LoaiSPId { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập Tên Danh mục")]
        public string Name { get; set; }
    }
}
