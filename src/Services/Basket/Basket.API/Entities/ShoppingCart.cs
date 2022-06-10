using System.Collections.Generic;

namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
        public ShoppingCart(string userName)
        {
            this.UserName = userName;
        }
        public ShoppingCart()
        {

        }

        public decimal TotalPrice 
        {
            get { 
                decimal total = 0;
                foreach (ShoppingCartItem item in Items) {
                    total += item.Price * item.Quantity;
                }
                return total;
            }
        }
    }
}
