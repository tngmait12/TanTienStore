using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TanTienStore.Data.Validation;

namespace TanTienStore.Models
{
    public class SanPhamModel
    {
        [Key]
        public int MaSP { get; set; }
        public string TenSanPham { get; set; }
        public string DonViTinh { get; set; }

        public decimal GiaBan { get; set; }
        public decimal GiaNhap { get; set; }

        public int LoaiSanPhamId { get; set; }

        public LoaiSanPhamModel LoaiSanPham { get; set; }

        public string HinhAnh { get; set; }
        public int SoLuong { get; set; }

        [NotMapped]
        [FileExtension]
        public IFormFile? ImageUpload { get; set; }
    }
}
