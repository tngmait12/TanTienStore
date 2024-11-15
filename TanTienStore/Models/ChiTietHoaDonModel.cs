﻿using System.ComponentModel.DataAnnotations.Schema;

namespace TanTienStore.Models
{
    public class ChiTietHoaDonModel
    {
		public int Id { get; set; }
		public string UserName { get; set; }
		public string OrderCode { get; set; }
		public int ProductId { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
		[ForeignKey("ProductId")]
		public SanPhamModel SanPham { get; set; }
	}
}
