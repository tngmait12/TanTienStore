using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TanTienStore.Data;
using TanTienStore.Helper;
using TanTienStore.Models;

namespace TanTienStore.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly DataContext _context;

        public SanPhamController(DataContext context)
        {
            _context = context;
        }

        // GET: SanPham
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.SanPhams.Include(s => s.LoaiSanPham);
            return View(await dataContext.ToListAsync());
        }

        // GET: SanPham/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPhamModel = await _context.SanPhams
                .Include(s => s.LoaiSanPham)
                .FirstOrDefaultAsync(m => m.MaSP == id);
            if (sanPhamModel == null)
            {
                return NotFound();
            }

            return View(sanPhamModel);
        }

        // GET: SanPham/Create
        public IActionResult Create()
        {
            ViewData["LoaiSanPhamId"] = new SelectList(_context.LoaiSanPhams, "LoaiSPId", "Name");
            return View();
        }

        // POST: SanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSP,TenSanPham,DonViTinh,GiaBan,LoaiSanPhamId,HinhAnh,SoLuong")] SanPhamModel sanPhamModel, IFormFile ImgUpload)
        {
            if (ImgUpload != null && ImgUpload.Length > 0)
            {
                var ImageName = ImageHelper.UpLoadImage(ImgUpload, "images");
                sanPhamModel.HinhAnh = ImageName;
            }
            else
            {
                sanPhamModel.HinhAnh = "";
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(sanPhamModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            ViewData["LoaiSanPhamId"] = new SelectList(_context.LoaiSanPhams, "LoaiSPId", "Name", sanPhamModel.LoaiSanPhamId);
            return View(sanPhamModel);
        }

        // GET: SanPham/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPhamModel = await _context.SanPhams.FindAsync(id);
            if (sanPhamModel == null)
            {
                return NotFound();
            }
            ViewData["LoaiSanPhamId"] = new SelectList(_context.LoaiSanPhams, "LoaiSPId", "Name", sanPhamModel.LoaiSanPhamId);
            return View(sanPhamModel);
        }

        // POST: SanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaSP,TenSanPham,DonViTinh,GiaBan,LoaiSanPhamId,HinhAnh,SoLuong")] SanPhamModel sanPhamModel, IFormFile ImgUpload)
        {
            if (id != sanPhamModel.MaSP)
            {
                return NotFound();
            }

            if (ImgUpload != null && ImgUpload.Length > 0)
            {
                var ImageName = ImageHelper.UpLoadImage(ImgUpload, "images");
                sanPhamModel.HinhAnh = ImageName;
            }
            else
            {
                var existingSanPham = await _context.SanPhams.AsNoTracking().FirstOrDefaultAsync(t => t.MaSP == sanPhamModel.MaSP);
                if (existingSanPham != null)
                {
                    sanPhamModel.HinhAnh = existingSanPham.HinhAnh;
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sanPhamModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanPhamModelExists(sanPhamModel.MaSP))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewData["LoaiSanPhamId"] = new SelectList(_context.LoaiSanPhams, "LoaiSPId", "Name", sanPhamModel.LoaiSanPhamId);
            return View(sanPhamModel);
        }

        // GET: SanPham/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPhamModel = await _context.SanPhams
                .Include(s => s.LoaiSanPham)
                .FirstOrDefaultAsync(m => m.MaSP == id);
            if (sanPhamModel == null)
            {
                return NotFound();
            }

            return View(sanPhamModel);
        }

        // POST: SanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sanPhamModel = await _context.SanPhams.FindAsync(id);
            if (sanPhamModel != null)
            {
                _context.SanPhams.Remove(sanPhamModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SanPhamModelExists(int id)
        {
            return _context.SanPhams.Any(e => e.MaSP == id);
        }
    }
}
