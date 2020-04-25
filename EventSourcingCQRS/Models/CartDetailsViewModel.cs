using System.Collections.Generic;
using System.Linq;
using EventSourcingCQRS.ReadModel.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventSourcingCQRS.Models
{
    public class CartDetailsViewModel : ViewModelBase
    {
        public Cart Cart { get; }

        public IEnumerable<CartItem> CartItems { get; }

        public IEnumerable<Product> Products { get; }

        public IEnumerable<SelectListItem> AvailableProducts
        {
            get
            {
                return Products.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id
                })
                .ToList();
            }
        }

        public CartDetailsViewModel(Cart cart, IEnumerable<CartItem> cartItems, 
            IEnumerable<Product> products)
        {
            Cart = cart;
            CartItems = cartItems;
            Products = products;
        }
    }
}
