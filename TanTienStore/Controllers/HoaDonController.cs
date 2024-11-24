using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TanTienStore.Data;

namespace TanTienStore.Controllers
{
	public class HoaDonController : Controller
	{
		private readonly DataContext _datacontext;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public HoaDonController(DataContext context, IWebHostEnvironment webhostenvironment)
		{
			_datacontext = context;
			_webHostEnvironment = webhostenvironment;

		}
		public async Task<IActionResult> Index()
		{
			var hoaDons = await _datacontext.HoaDons.Include(h => h.KhachHang)
				.ToListAsync();


			return View(hoaDons);
		}
	}
}
