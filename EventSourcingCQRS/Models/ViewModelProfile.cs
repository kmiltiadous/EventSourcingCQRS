using AutoMapper;
using EventSourcingCQRS.ReadModel.Models;

namespace EventSourcingCQRS.Models
{
    public class ViewModelProfile : Profile
    {
        public ViewModelProfile()
        {
            CreateMap<CartDetails, CartDetailsViewModel>()
                .ConstructUsing(c => new CartDetailsViewModel(c.Cart, c.CartItems, c.Products));
            CreateMap<CartSummary, CartIndexViewModel>()
                .ConstructUsing(c => new CartIndexViewModel(c.Carts, c.Customers));
        }
    }
}
