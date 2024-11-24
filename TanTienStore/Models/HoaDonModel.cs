using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TanTienStore.Models
{
	public class HoaDonModel
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int MaHD { get; set; }

		[ForeignKey("KhachHang")]
		public int MaKH { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime NgayLap { get; set; }

		[Required]
		[Column(TypeName = "decimal(18,2)")]
		public decimal TongTienTruocCK { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal? ChietKhau { get; set; }

		[Required]
		[Column(TypeName = "decimal(18,2)")]
		public decimal ThanhTien { get; set; }

		[StringLength(50)]
		public string? PhuongThucThanhToan { get; set; }

		// Navigation Properties
		public virtual KhachHangModel KhachHang { get; set; } // Điều hướng tới Khách Hàng
		public virtual ICollection<ChiTietHoaDonModel> ChiTietHoaDons { get; set; } // Chi tiết hóa đơn
	}
}
