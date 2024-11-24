using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TanTienStore.Models;

namespace TanTienStore.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<SanPhamModel> SanPhams { get; set; }
        public DbSet<LoaiSanPhamModel> LoaiSanPhams { get; set; }
        public DbSet<KhachHangModel> KhachHang { get; set; }
        public DbSet<HoaDonModel> HoaDons { get; set; }
        public DbSet<ChiTietHoaDonModel> ChiTietHoaDons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình khóa chính hỗn hợp cho ChiTietHoaDonModel
            modelBuilder.Entity<ChiTietHoaDonModel>()
                .HasKey(c => new { c.MaHD, c.MaSP });

            // Cấu hình quan hệ với HoaDonModel
            modelBuilder.Entity<ChiTietHoaDonModel>()
                .HasOne(c => c.HoaDon)
                .WithMany(h => h.ChiTietHoaDons)
                .HasForeignKey(c => c.MaHD);

            // Cấu hình quan hệ với SanPhamModel
            modelBuilder.Entity<ChiTietHoaDonModel>()
                .HasOne(c => c.SanPham)
                .WithMany(p => p.ChiTietHoaDons)
                .HasForeignKey(c => c.MaSP);
        }
    }
}
