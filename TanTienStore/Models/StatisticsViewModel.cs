namespace TanTienStore.Models
{
    public class StatisticsViewModel
    {
        public string Period { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalInvoices { get; set; }
        public int TotalProductsSold { get; set; }
        public decimal TotalDiscount { get; set; }

        public bool IsDailyView { get; set; }
        public bool IsMonthlyView { get; set; }
        public bool IsYearlyView { get; set; }

        public List<string> DateLabels { get; set; }
        public List<decimal> RevenueData { get; set; }

        // Thêm các thuộc tính cho các biểu đồ mới
        public List<int> InvoiceCounts { get; set; }
        public List<int> ProductsSoldData { get; set; }
        public List<decimal> DiscountData { get; set; }
    }






}
