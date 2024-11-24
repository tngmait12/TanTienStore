namespace TanTienStore.Models.ViewModel
{
	public class SanPhamCartViewModel
	{
		public IEnumerable<SanPhamModel> SanPhams { get; set; }
		public CartItemViewModel Cart { get; set; }
	}
}
