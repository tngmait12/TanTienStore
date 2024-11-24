using System;

namespace TanTienStore.Models
{
    public class CartItemModel
    {
        public int MaSP { get; set; }
        public string TenSanPham { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaBan { get; set; }
        public decimal TongTien
        {
            get { return SoLuong * GiaBan; }
        }
        public string? HinhAnh { get; set; }

        // Constructor mặc định
        public CartItemModel()
        {
        }

        // Constructor nhận dữ liệu từ SanPhamModel
        public CartItemModel(SanPhamModel sanPham)
        {
            MaSP = sanPham.MaSP;
            TenSanPham = sanPham.TenSanPham;
            GiaBan = sanPham.GiaBan;
            SoLuong = 1; // Mặc định số lượng là 1 khi thêm sản phẩm vào giỏ hàng
            HinhAnh = sanPham.HinhAnh;
        }
    }
}
