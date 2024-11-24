using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TanTienStore.Data;
using TanTienStore.Models;
using Web_Bán_Hàng.Database;

namespace TanTienStore.Controllers
{
    public class TaoDonHangController : Controller
    {
        private readonly DataContext _datacontext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TaoDonHangController(DataContext context, IWebHostEnvironment webhostenvironment)
        {
            _datacontext = context;
            _webHostEnvironment = webhostenvironment;

        }
        public IActionResult Index(string keyword, int pageNumber = 1, int pageSize = 4 )
        {
            // Lấy danh sách sản phẩm từ cơ sở dữ liệu
            var query = _datacontext.SanPhams.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(p => p.TenSanPham.Contains(keyword));
                ViewData["Keyword"] = keyword;
            }

            // Tính tổng số trang
            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            // Lấy dữ liệu cho trang hiện tại
            var items = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Gửi thông tin sang View
            ViewData["PageNumber"] = pageNumber;
            ViewData["TotalPages"] = totalPages;
           


            return View(items);
        }

        public async Task<IActionResult> DSHoadon(int page = 1, string search = "")
        {
            int pageSize = 10; // Số lượng hóa đơn mỗi trang
            var query = _datacontext.HoaDons.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(hd => hd.MaHD.ToString().Contains(search));
                ViewData["SearchQuery"] = search;
            }

            // Tổng số hóa đơn sau khi áp dụng bộ lọc
            int totalRecords = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            // Kiểm tra nếu trang vượt ngoài phạm vi
            if (page < 1) page = 1;
            else if (page > totalPages) page = totalPages;

            // Lấy dữ liệu phân trang
            var hoaDons = await query
                .OrderByDescending(hd => hd.NgayLap)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Truyền dữ liệu phân trang sang ViewData
            ViewData["TotalPages"] = totalPages;
            ViewData["CurrentPage"] = page;

            return View(hoaDons);
        }




        [HttpPost]
		/*public async Task<IActionResult> CreateOrder(int maKH)
		{
			// Kiểm tra xem khách hàng có tồn tại không
			var khachHang = await _datacontext.KhachHang.FindAsync(maKH);
			if (khachHang == null)
			{
				TempData["error"] = "Mã khách hàng không hợp lệ!";
				return RedirectToAction("Index", "Home");
			}

			// Lấy giỏ hàng từ Session
			var cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
			if (cart == null || !cart.Any())
			{
				TempData["error"] = "Giỏ hàng rỗng. Không thể tạo hóa đơn!";
				return RedirectToAction("Index", "Home");
			}

			// Tạo hóa đơn mới
			var hoaDon = new HoaDonModel
			{
				MaKH = maKH, // Mã khách hàng từ form
				NgayLap = DateTime.Now,
				TongTienTruocCK = cart.Sum(item => item.SoLuong * item.GiaBan),
				ChietKhau = 0,
				ThanhTien = cart.Sum(item => item.SoLuong * item.GiaBan),
				PhuongThucThanhToan = "Tiền mặt"
			};

			// Thêm hóa đơn vào database
			_datacontext.HoaDons.Add(hoaDon);
			await _datacontext.SaveChangesAsync();

			// Lưu chi tiết hóa đơn
			foreach (var item in cart)
			{
				var chiTietHoaDon = new ChiTietHoaDonModel
				{
					MaHD = hoaDon.MaHD,
					MaSP = item.MaSP,
					SoLuong = item.SoLuong,
					DonGia = item.GiaBan
				};

				_datacontext.ChiTietHoaDons.Add(chiTietHoaDon);
			}

			await _datacontext.SaveChangesAsync();

			// Xóa giỏ hàng khỏi Session
			HttpContext.Session.Remove("Cart");

			TempData["success"] = "Hóa đơn đã được tạo thành công!";
			return RedirectToAction("Index", "HoaDon");
		}*/


		// Phương thức Add để thêm sản phẩm vào giỏ
		[HttpPost]
        public async Task<IActionResult> Add(int id, int soluong)
        {
            if (soluong <= 0)
            {
                TempData["error"] = "Số lượng không hợp lệ!";
                return Redirect(Request.Headers["Referer"].ToString() ?? Url.Action("Index", "Home"));
            }

            // Lấy sản phẩm từ cơ sở dữ liệu
            var product = await _datacontext.SanPhams.FindAsync(id);
            if (product == null)
            {
                TempData["error"] = "Sản phẩm không tồn tại!";
                return Redirect(Request.Headers["Referer"].ToString() ?? Url.Action("Index", "Home"));
            }

            // Lấy giỏ hàng từ Session
            var cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();

            // Tìm sản phẩm trong giỏ hàng
            var cartItem = cart.FirstOrDefault(c => c.MaSP == id);

            if (cartItem == null)
            {
                // Thêm sản phẩm mới vào giỏ hàng
                cart.Add(new CartItemModel
                {
                    MaSP = product.MaSP,
                    TenSanPham = product.TenSanPham,
                    GiaBan = product.GiaBan,
                    SoLuong = soluong
                });
            }
            else
            {
                // Cập nhật số lượng sản phẩm hiện có trong giỏ hàng
                cartItem.SoLuong += soluong;
            }

            // Lưu giỏ hàng vào Session
            HttpContext.Session.SetJson("Cart", cart);

            TempData["success"] = $"Thêm {soluong} sản phẩm vào giỏ hàng thành công!";
            return Redirect(Request.Headers["Referer"].ToString() ?? Url.Action("Index", "Home"));
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrderWithDetails(int makh, string phuongThucThanhToan,decimal chietKhau)
        {
            // Lấy giỏ hàng từ session
            var cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();

            if (cartItems.Count == 0)
            {
                TempData["error"] = "Giỏ hàng trống! Không thể tạo hóa đơn.";
                return RedirectToAction("Index", "Cart");
            }

            // Tạo hóa đơn mới
            var hoaDon = new HoaDonModel
            {
                NgayLap = DateTime.Now,
                MaKH = makh,                // Mã khách hàng nhận từ form
                PhuongThucThanhToan = phuongThucThanhToan  // Phương thức thanh toán nhận từ form
            };

            // Thêm hóa đơn vào cơ sở dữ liệu
            _datacontext.HoaDons.Add(hoaDon);
            await _datacontext.SaveChangesAsync();

            decimal tongTienHoaDon = 0; // Khai báo một lần

            // Tạo chi tiết hóa đơn từ giỏ hàng
            foreach (var item in cartItems)
            {
                var chiTiet = new ChiTietHoaDonModel
                {
                    MaHD = hoaDon.MaHD,  // Gán mã hóa đơn cho chi tiết hóa đơn
                    MaSP = item.MaSP,    // Mã sản phẩm
                    SoLuong = item.SoLuong,
                    DonGia = item.GiaBan,
                    TongTien = item.SoLuong * item.GiaBan,
                };

                // Trong vòng lặp
                tongTienHoaDon += chiTiet.TongTien; // Cộng thêm tổng tiền vào biến



                _datacontext.ChiTietHoaDons.Add(chiTiet);
            }

            // Lưu chi tiết hóa đơn vào cơ sở dữ liệu
            await _datacontext.SaveChangesAsync();
            // Cập nhật tổng tiền trước chiết khấu cho hóa đơn
            hoaDon.TongTienTruocCK = tongTienHoaDon;

            // Kiểm tra chiết khấu và tính toán tổng tiền sau khi chiết khấu
            if (chietKhau == 0)
            {
                hoaDon.ThanhTien = tongTienHoaDon; // Nếu chiết khấu = 0, tổng tiền = tổng tiền trước chiết khấu
            }
            else
            {
                hoaDon.ThanhTien = tongTienHoaDon - (tongTienHoaDon * (chietKhau / 100)); // Tính chiết khấu và trừ đi
            }
            hoaDon.ChietKhau = chietKhau;
            // Cập nhật thông tin hóa đơn trong cơ sở dữ liệu
            _datacontext.HoaDons.Update(hoaDon);
            await _datacontext.SaveChangesAsync();


            // Cập nhật điểm tích lũy cho khách hàng
            var khachHang = await _datacontext.KhachHang.FindAsync(makh); // Tìm khách hàng bằng MaKH
            if (khachHang != null)
            {
                khachHang.DiemTichLuy += 5; // Cộng thêm 5 điểm
                _datacontext.KhachHang.Update(khachHang); // Cập nhật vào cơ sở dữ liệu
                await _datacontext.SaveChangesAsync(); // Lưu thay đổi
            }


            // Xóa giỏ hàng sau khi tạo hóa đơn và chi tiết hóa đơn
            HttpContext.Session.Remove("Cart");

            TempData["success"] = "Hóa đơn và chi tiết hóa đơn đã được tạo thành công!";
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> ListHoaDon()
        {
            // Truy vấn tất cả hóa đơn
            var hoaDons = await _datacontext.HoaDons
                .Include(hd => hd.ChiTietHoaDons) // Nếu cần load chi tiết hóa đơn
                .OrderByDescending(hd => hd.NgayLap)
                .ToListAsync();

            return View(hoaDons);
        }

        public async Task<IActionResult> Details(int id)
        {
            var hoaDon = await _datacontext.HoaDons
                .Include(h => h.ChiTietHoaDons)
                .ThenInclude(c => c.SanPham)
                .FirstOrDefaultAsync(h => h.MaHD == id);

            if (hoaDon == null)
            {
                TempData["error"] = "Không tìm thấy hóa đơn!";
                return RedirectToAction("ListHoaDon", "TaoDonHang");
            }

            // Tính toán tổng tiền trước chiết khấu
            hoaDon.TongTienTruocCK = hoaDon.ChiTietHoaDons.Sum(c => c.TongTien);

            // Tính toán tổng tiền sau chiết khấu
            // Sử dụng ?? 0 để xử lý trường hợp ChietKhau có thể là null
            hoaDon.ThanhTien = hoaDon.TongTienTruocCK - (hoaDon.TongTienTruocCK * ((hoaDon.ChietKhau ?? 0) / 100));

            return View(hoaDon);
        }










    }






}

