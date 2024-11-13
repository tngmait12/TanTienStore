namespace TanTienStore.Models
{
    public class HoaDonModel
    {
        public int Id { get; set; }
        public string OrderCode { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Status { get; set; }
        public string? PaymentMethod { get; set; }
    }
}
