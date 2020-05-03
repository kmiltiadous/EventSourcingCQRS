using System.Collections.ObjectModel;

namespace Mobile.Client.Models
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

