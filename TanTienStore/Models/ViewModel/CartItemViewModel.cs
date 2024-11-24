using System.Collections.Generic;

namespace TanTienStore.Models.ViewModel
{
    public class CartItemViewModel
    {
        public List<CartItemModel> CartItems { get; set; } = new List<CartItemModel>();

        public decimal GrandTotal
        {
            get; set;
        }
    }
}
