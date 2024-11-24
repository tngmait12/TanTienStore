using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TanTienStore.Models
{
	public class ChiTietHoaDonModel
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int MaChiTietHD { get; set; } // Thêm khóa chính riêng cho bảng chi tiết

		[ForeignKey("HoaDon")]
		public int MaHD { get; set; }

		[ForeignKey("SanPham")]
		public int MaSP { get; set; }

		[Required]
		public int SoLuong { get; set; }

		[Required]
		[Column(TypeName = "decimal(18,2)")]
		public decimal DonGia { get; set; }
		public decimal TongTien {  get; set; }

		// Navigation Properties
		public virtual HoaDonModel HoaDon { get; set; }
		public virtual SanPhamModel SanPham { get; set; }
	}
}
