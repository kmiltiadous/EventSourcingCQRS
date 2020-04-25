using System.Collections.Generic;

namespace EventSourcingCQRS.ReadModel.Models
{
    public class CartSummary
    {
        public IEnumerable<Cart> Carts { get; set; }

        public IEnumerable<Customer> Customers { get; set; }
    }
}
