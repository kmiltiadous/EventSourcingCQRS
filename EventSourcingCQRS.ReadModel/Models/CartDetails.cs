using System.Collections.Generic;

namespace EventSourcingCQRS.ReadModel.Models
{
    public class CartDetails
    {
        public Cart Cart { get; set; }

        public IEnumerable<CartItem> CartItems { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
