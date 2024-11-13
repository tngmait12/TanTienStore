using Microsoft.AspNetCore.Mvc;

namespace TanTienStore.Controllers
{
    public class SanPhamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
