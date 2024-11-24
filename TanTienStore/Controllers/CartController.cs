using Microsoft.AspNetCore.Mvc;
using TanTienStore.Data;
using TanTienStore.Models.ViewModel;
using TanTienStore.Models;
using Web_Bán_Hàng.Database;

namespace TanTienStore.Controllers
{
	public class CartController : Controller
	{
		private readonly DataContext _datacontext;
		public CartController(DataContext context)
		{
			_datacontext = context;

		}
		public IActionResult Index()
		{
			List<CartItemModel> cartItem = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			CartItemViewModel cart = new()
			{
				CartItems = cartItem,
				GrandTotal = cartItem.Sum(p => p.SoLuong * p.GiaBan)

			};

			return View(cart);
		}

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
				// Cập nhật số lượng sản phẩm hiện có
				cartItem.SoLuong += soluong;
			}

			// Lưu giỏ hàng vào Session
			HttpContext.Session.SetJson("Cart", cart);

			TempData["success"] = $"Thêm {soluong} sản phẩm vào giỏ hàng thành công!";
			return Redirect(Request.Headers["Referer"].ToString() ?? Url.Action("Index", "Home"));
		}

		public async Task<IActionResult> Decrease(int id)
		{
			// Lấy giỏ hàng từ Session
			var cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();

			// Tìm sản phẩm trong giỏ hàng
			var cartItem = cart.FirstOrDefault(c => c.MaSP == id);

			if (cartItem == null)
			{
				TempData["error"] = "Sản phẩm không tồn tại trong giỏ hàng!";
				return RedirectToAction("Index", "Cart");
			}

			// Giảm số lượng hoặc xóa sản phẩm
			if (cartItem.SoLuong > 1)
			{
				cartItem.SoLuong--;
				TempData["success"] = "Giảm số lượng sản phẩm thành công!";
			}
			else
			{
				cart.Remove(cartItem);
				TempData["success"] = "Xóa sản phẩm khỏi giỏ hàng thành công!";
			}

			// Cập nhật lại Session hoặc xóa Session nếu giỏ hàng trống
			if (cart.Any())
			{
				HttpContext.Session.SetJson("Cart", cart);
			}
			else
			{
				HttpContext.Session.Remove("Cart");
			}

			return RedirectToAction("Index", "Cart");
		}


		public async Task<IActionResult> Increase(int id)
		{
			// Lấy giỏ hàng từ Session
			var cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();

			// Tìm sản phẩm cần tăng số lượng
			var cartItem = cart.FirstOrDefault(c => c.MaSP == id);

			if (cartItem != null)
			{
				// Tăng số lượng sản phẩm
				cartItem.SoLuong++;
				TempData["success"] = "Tăng số lượng sản phẩm thành công!";
			}
			else
			{
				TempData["error"] = "Không tìm thấy sản phẩm trong giỏ hàng!";
			}

			// Cập nhật giỏ hàng trong Session
			HttpContext.Session.SetJson("Cart", cart);

			// Chuyển hướng về trang giỏ hàng
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Remove(int id)
		{
			// Lấy giỏ hàng từ Session
			var cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();

			// Xóa sản phẩm khỏi giỏ hàng
			var removedCount = cart.RemoveAll(p => p.MaSP == id);

			if (removedCount > 0)
			{
				TempData["success"] = "Đã xóa sản phẩm khỏi giỏ hàng!";
			}
			else
			{
				TempData["error"] = "Không tìm thấy sản phẩm để xóa!";
			}

			// Cập nhật lại Session hoặc xóa nếu giỏ hàng trống
			if (cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", cart);
			}

			// Chuyển hướng về trang giỏ hàng
			return RedirectToAction("Index");
		}

		public IActionResult Clear()
		{
			// Xóa toàn bộ giỏ hàng khỏi Session
			HttpContext.Session.Remove("Cart");

			TempData["success"] = "Đã xóa toàn bộ giỏ hàng!";
			return RedirectToAction("Index");
		}




	}
}
