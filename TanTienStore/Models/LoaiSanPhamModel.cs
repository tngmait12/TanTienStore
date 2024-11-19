using System.ComponentModel.DataAnnotations;

namespace TanTienStore.Models
{
    public class LoaiSanPhamModel
    {
        [Key]
        public int LoaiSPId { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập tên loại sản phẩm"), MaxLength(50)]
        public string Name { get; set; }
    }
}
