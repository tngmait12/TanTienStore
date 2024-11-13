using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TanTienStore.Data;
using TanTienStore.Models;

namespace TanTienStore.Controllers
{
    public class LoaiSanPhamController : Controller
    {
        private readonly DataContext _dataContext;
        public LoaiSanPhamController(DataContext context)
        {
            _dataContext = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.LoaiSanPhams.OrderByDescending(p => p.LoaiSPId).ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoaiSanPhamModel loaisp)
        {
            if (ModelState.IsValid)
            {
                _dataContext.Add(loaisp);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Thêm danh mục thành công!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Model đang có một vài thứ bị lỗi!!!";
                List<string> errors = new List<string>();
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                string errorMessage = string.Join("\n", errors);
                return BadRequest(errorMessage);
            }

            return View(loaisp);
        }
        public async Task<IActionResult> Edit(int Id)
        {
            LoaiSanPhamModel loaisp = await _dataContext.LoaiSanPhams.FirstOrDefaultAsync(p => p.LoaiSPId == Id);
            return View(loaisp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, LoaiSanPhamModel loaisp)
        {

            var existed_loaisp = _dataContext.LoaiSanPhams.FirstOrDefault(p=>p.LoaiSPId==Id);
            if (ModelState.IsValid)
            {


                existed_loaisp.Name = loaisp.Name;
                //

                _dataContext.Update(existed_loaisp);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Cập nhật danh mục thành công!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Model đang có một vài thứ bị lỗi!!!";
                List<string> errors = new List<string>();
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                string errorMessage = string.Join("\n", errors);
                return BadRequest(errorMessage);
            }

            return View(loaisp);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            LoaiSanPhamModel loaisp = await _dataContext.LoaiSanPhams.FirstOrDefaultAsync(p => p.LoaiSPId == Id);

            if (loaisp == null)
            {
                return NotFound();
            }
            _dataContext.LoaiSanPhams.Remove(loaisp);
            await _dataContext.SaveChangesAsync();
            TempData["success"] = "Xóa danh mục thành công";

            return RedirectToAction("Index");
        }
    }
}
