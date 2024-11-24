using Microsoft.AspNetCore.Mvc;
using TanTienStore.Data;
using TanTienStore.Models;
using System.Linq;

public class HomeController : Controller
{
    private readonly DataContext _context;

    public HomeController(DataContext context)
    {
        _context = context;
    }

    public IActionResult Index(string viewType = "daily")
    {
        var model = new StatisticsViewModel();

        // Thiết lập chế độ hiển thị
        model.IsDailyView = viewType == "daily";
        model.IsMonthlyView = viewType == "monthly";
        model.IsYearlyView = viewType == "yearly";

        // Truy vấn chung để kết hợp HoaDons và ChiTietHoaDons
        var chiTietHoaDons = _context.ChiTietHoaDons
            .Join(_context.HoaDons,
                ct => ct.MaHD,
                hd => hd.MaHD,
                (ct, hd) => new { hd.NgayLap, ct.SoLuong })
            .ToList();

        if (model.IsDailyView)
        {
            // Labels cho Daily View
            model.DateLabels = _context.HoaDons
                .GroupBy(hd => hd.NgayLap.Date)
                .Select(g => g.Key.ToString("dd-MM-yyyy"))
                .ToList();

            // Revenue Data
            model.RevenueData = _context.HoaDons
                .GroupBy(hd => hd.NgayLap.Date)
                .Select(g => g.Sum(hd => hd.TongTienTruocCK))
                .ToList();

            // Invoice Counts
            model.InvoiceCounts = _context.HoaDons
                .GroupBy(hd => hd.NgayLap.Date)
                .Select(g => g.Count())
                .ToList();

            // Products Sold Data
            model.ProductsSoldData = chiTietHoaDons
                .GroupBy(x => x.NgayLap.Date)
                .Select(g => g.Sum(x => x.SoLuong))
                .ToList();

            // Discount Data
            model.DiscountData = _context.HoaDons
                .GroupBy(hd => hd.NgayLap.Date)
                .Select(g => g.Sum(hd => hd.ChietKhau ?? 0))
                .ToList();
        }
        else if (model.IsMonthlyView)
        {
            // Labels cho Monthly View
            model.DateLabels = _context.HoaDons
                .GroupBy(hd => new { hd.NgayLap.Year, hd.NgayLap.Month })
                .Select(g => g.Key.Year + "-" + g.Key.Month.ToString("D2"))
                .ToList();

            // Revenue Data
            model.RevenueData = _context.HoaDons
                .GroupBy(hd => new { hd.NgayLap.Year, hd.NgayLap.Month })
                .Select(g => g.Sum(hd => hd.TongTienTruocCK))
                .ToList();

            // Invoice Counts
            model.InvoiceCounts = _context.HoaDons
                .GroupBy(hd => new { hd.NgayLap.Year, hd.NgayLap.Month })
                .Select(g => g.Count())
                .ToList();

            // Products Sold Data
            model.ProductsSoldData = chiTietHoaDons
                .GroupBy(x => new { x.NgayLap.Year, x.NgayLap.Month })
                .Select(g => g.Sum(x => x.SoLuong))
                .ToList();

            // Discount Data
            model.DiscountData = _context.HoaDons
                .GroupBy(hd => new { hd.NgayLap.Year, hd.NgayLap.Month })
                .Select(g => g.Sum(hd => hd.ChietKhau ?? 0))
                .ToList();
        }
        else if (model.IsYearlyView)
        {
            // Labels cho Yearly View
            model.DateLabels = _context.HoaDons
                .GroupBy(hd => hd.NgayLap.Year)
                .Select(g => g.Key.ToString())
                .ToList();

            // Revenue Data
            model.RevenueData = _context.HoaDons
                .GroupBy(hd => hd.NgayLap.Year)
                .Select(g => g.Sum(hd => hd.TongTienTruocCK))
                .ToList();

            // Invoice Counts
            model.InvoiceCounts = _context.HoaDons
                .GroupBy(hd => hd.NgayLap.Year)
                .Select(g => g.Count())
                .ToList();

            // Products Sold Data
            model.ProductsSoldData = chiTietHoaDons
                .GroupBy(x => x.NgayLap.Year)
                .Select(g => g.Sum(x => x.SoLuong))
                .ToList();

            // Discount Data
            model.DiscountData = _context.HoaDons
                .GroupBy(hd => hd.NgayLap.Year)
                .Select(g => g.Sum(hd => hd.ChietKhau ?? 0))
                .ToList();
        }

        // Thống kê tổng doanh thu, hóa đơn, sản phẩm bán ra và chiết khấu
        model.TotalRevenue = _context.HoaDons.Sum(hd => hd.TongTienTruocCK);
        model.TotalInvoices = _context.HoaDons.Count();
        model.TotalProductsSold = _context.ChiTietHoaDons.Sum(ct => ct.SoLuong);
        model.TotalDiscount = _context.HoaDons.Sum(hd => hd.ChietKhau ?? 0);

        return View(model);
    }
}
