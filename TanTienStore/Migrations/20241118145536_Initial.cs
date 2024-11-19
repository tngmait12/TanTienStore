using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TanTienStore.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoaiSanPhams",
                columns: table => new
                {
                    LoaiSPId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiSanPhams", x => x.LoaiSPId);
                });

            migrationBuilder.CreateTable(
                name: "SanPhams",
                columns: table => new
                {
                    MaSP = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenSanPham = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DonViTinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GiaBan = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    LoaiSanPhamId = table.Column<int>(type: "int", nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhams", x => x.MaSP);
                    table.ForeignKey(
                        name: "FK_SanPhams_LoaiSanPhams_LoaiSanPhamId",
                        column: x => x.LoaiSanPhamId,
                        principalTable: "LoaiSanPhams",
                        principalColumn: "LoaiSPId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SanPhams_LoaiSanPhamId",
                table: "SanPhams",
                column: "LoaiSanPhamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SanPhams");

            migrationBuilder.DropTable(
                name: "LoaiSanPhams");
        }
    }
}
