using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EventSourcingCQRS.Models;
using EventSourcingCQRS.Services;

namespace EventSourcingCQRS.Controllers
{
    public class CartsController : Controller
    {
        private readonly ICustomerService customerService;
        private readonly IProductService productService;
        private readonly ICartService cartService;

        public CartsController(
            ICustomerService customerService,
            IProductService productService,
            ICartService cartService)
        {
            this.customerService = customerService;
            this.productService = productService;
            this.cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            var cartDetails  = await cartService.GetCarts();
            var customers = await customerService.GetCustomers();
            var carts = cartDetails.Select(c => c.Cart);

            var viewModel = new CartIndexViewModel(carts, customers);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(string customerId)
        {
            await cartService.CreateCart(customerId, null);
            var cartDetails = await cartService.GetCarts();
            var customers = await customerService.GetCustomers();
            var carts = cartDetails.Select(c => c.Cart);

            var viewModel = new CartIndexViewModel(carts, customers);

            return View(nameof(IndexAsync), viewModel);
        }

        [Route("{id:length(41)}")]
        public async Task<IActionResult> DetailsAsync(string id)
        {
            var cart = await cartService.GetCart(id);
            var items = await cartService.GetCartItems(id);
            var products= await productService.GetProducts();

            var viewModel = new CartDetailsViewModel(cart, items, products);
 
            return View(viewModel);
        }

        [Route("{id:length(41)}/AddProduct")]
        [HttpPost]
        public async Task<IActionResult> AddProductAsync(string id, string productId, int quantity)
        {
            await cartService.AddToCart(id, productId, quantity);
            var cart = await cartService.GetCart(id);
            var items = await cartService.GetCartItems(id);
            var products = await productService.GetProducts();

            var viewModel = new CartDetailsViewModel(cart, items, products);

            return View(nameof(DetailsAsync), viewModel);
        }

        [Route("{id:length(41)}/ChangeProductQuantity")]
        [HttpPost]
        public async Task<IActionResult> ChangeProductQuantityAsync(string id, string productId, int quantity)
        {
            await cartService.ChangeQuantity(id, productId, quantity);
            var cart = await cartService.GetCart(id);
            var items = await cartService.GetCartItems(id);
            var products = await productService.GetProducts();

            var viewModel = new CartDetailsViewModel(cart, items, products);

            return View(nameof(DetailsAsync), viewModel);
        }

    }
}