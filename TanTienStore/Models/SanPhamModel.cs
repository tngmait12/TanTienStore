using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TanTienStore.Data.Validation;

namespace TanTienStore.Models
{
    public class SanPhamModel
    {
        [Key]
        public int MaSP { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập tên sản phẩm"), MaxLength(100)]
        public string TenSanPham { get; set; }

        public string DonViTinh { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập giá sản phẩm")]
        [Range(0.01, double.MaxValue)]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal GiaBan { get; set; }

        public int? LoaiSanPhamId { get; set; }

        public LoaiSanPhamModel? LoaiSanPham { get; set; }

        public string? HinhAnh { get; set; }

        [DefaultValue(0)]
        public int SoLuong { get; set; }

        [NotMapped]
        [FileExtension]
        public IFormFile? ImageUpload { get; set; }

        // Thêm thuộc tính này để biểu diễn quan hệ với ChiTietHoaDonModel
        public ICollection<ChiTietHoaDonModel> ChiTietHoaDons { get; set; }
    }
}
