using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TanTienStore.Data;
using TanTienStore.Models;

namespace TanTienStore.Controllers
{
    public class LoaiSanPhamController : Controller
    {
        private readonly DataContext _context;

        public LoaiSanPhamController(DataContext context)
        {
            _context = context;
        }

        // GET: LoaiSanPham
        public async Task<IActionResult> Index()
        {
            return View(await _context.LoaiSanPhams.ToListAsync());
        }

        // GET: LoaiSanPham/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiSanPhamModel = await _context.LoaiSanPhams
                .FirstOrDefaultAsync(m => m.LoaiSPId == id);
            if (loaiSanPhamModel == null)
            {
                return NotFound();
            }

            return View(loaiSanPhamModel);
        }

        // GET: LoaiSanPham/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoaiSanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoaiSPId,Name")] LoaiSanPhamModel loaiSanPhamModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loaiSanPhamModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loaiSanPhamModel);
        }

        // GET: LoaiSanPham/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiSanPhamModel = await _context.LoaiSanPhams.FindAsync(id);
            if (loaiSanPhamModel == null)
            {
                return NotFound();
            }
            return View(loaiSanPhamModel);
        }

        // POST: LoaiSanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LoaiSPId,Name")] LoaiSanPhamModel loaiSanPhamModel)
        {
            if (id != loaiSanPhamModel.LoaiSPId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaiSanPhamModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiSanPhamModelExists(loaiSanPhamModel.LoaiSPId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(loaiSanPhamModel);
        }

        // GET: LoaiSanPham/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiSanPhamModel = await _context.LoaiSanPhams
                .FirstOrDefaultAsync(m => m.LoaiSPId == id);
            if (loaiSanPhamModel == null)
            {
                return NotFound();
            }

            return View(loaiSanPhamModel);
        }

        // POST: LoaiSanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loaiSanPhamModel = await _context.LoaiSanPhams.FindAsync(id);
            if (loaiSanPhamModel != null)
            {
                _context.LoaiSanPhams.Remove(loaiSanPhamModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaiSanPhamModelExists(int id)
        {
            return _context.LoaiSanPhams.Any(e => e.LoaiSPId == id);
        }
    }
}
