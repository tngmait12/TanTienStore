namespace TanTienStore.Models
{
    public class ThongKeModel
    {
		public int Id { get; set; }
		public int Quantity { get; set; } //so luong ban sp
		public int Sold { get; set; } //so luong order
		public decimal Revenue { get; set; } //doanh thu
		public decimal Profit { get; set; } //loi nhuan

		public DateTime DateCreated { get; set; } //ngay dat hang
	}
}
