using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TanTienStore.Data.Validation;

namespace TanTienStore.Models
{
    public class SanPhamModel
    {
        [Key]
        public int MaSP { get; set; }
        [Required, MinLength(4, ErrorMessage = "Yêu cầu nhập Tên Sản phẩm")]
        public string TenSanPham { get; set; }
        [Required, MinLength(4, ErrorMessage = "Yêu cầu nhập DVT Sản phẩm")]
        public string DonViTinh { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập Giá Sản phẩm")]
        [Range(0.01, double.MaxValue)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal GiaBan { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập Giá vốn Sản phẩm")]
        [Range(0.01, double.MaxValue)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal GiaNhap { get; set; }
        [Required, Range(1, int.MaxValue, ErrorMessage = "Chọn một danh mục")]

        public int LoaiSanPhamId { get; set; }

        public LoaiSanPhamModel LoaiSanPham { get; set; }

        public string Image { get; set; }
        public int Quantity { get; set; }

        [NotMapped]
        [FileExtension]
        public IFormFile? ImageUpload { get; set; }
    }
}
