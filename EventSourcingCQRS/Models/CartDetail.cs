using System.Collections.ObjectModel;

namespace EventSourcingCQRS.Models
{
    public class CartDetail : ObservableCollection<CartItem>
    {
        public Cart Cart { get; }
        public CartDetail(Cart cart)
        {
            Cart = cart;
        }
    }
}

