using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using EventSourcingCQRS.ReadModel.Models;

namespace EventSourcingCQRS.Models
{
    public class CartIndexViewModel :ViewModelBase
    {
        public IEnumerable<Cart> Carts { get; set; }

        public IEnumerable<Customer> Customers { get; set; }

        public IEnumerable<SelectListItem> AvailableCustomers
        {
            get
            {
                return Customers.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id
                })
                .ToList();
            }
        }

        public CartIndexViewModel(IEnumerable<Cart> carts, IEnumerable<Customer> customers)
        {
            Carts = carts;
            Customers = customers;
        }
    }
}
