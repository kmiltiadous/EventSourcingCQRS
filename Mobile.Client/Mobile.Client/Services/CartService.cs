using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mobile.Client.Models;
using Newtonsoft.Json.Linq;

namespace Mobile.Client.Services
{
    public class CartService : ICartService
    {
        private readonly IGenericService genericService;

        public CartService(IGenericService genericService)
        {
            this.genericService = genericService;
        }

        public async Task<IEnumerable<CartDetail>> GetCarts()
        {
            var builder = new UriBuilder(ApiEndpoints.BaseApiUrl)
            {
                Path = $"{ApiEndpoints.CartsEndpoint}"
            };
            var result = await genericService.GetAsync<ActionResponse<IEnumerable<Cart>>>(builder.ToString());

            if (!result.WasSuccessful) return new List<CartDetail>();
            var cartDetails = new List<CartDetail>();

            foreach (var cart in result.Value)
            {
                var cartDetail = new CartDetail(cart);

                builder = new UriBuilder(ApiEndpoints.BaseApiUrl)
                {
                    Path = string.Format(ApiEndpoints.CartItemsEndpoint, cart.Id)
                };

                var itemsResult = await genericService.GetAsync<ActionResponse<IEnumerable<CartItem>>>(builder.ToString());
                if (itemsResult.WasSuccessful)
                {
                    foreach (var cartItem in itemsResult.Value)
                    {
                        cartDetail.Add(cartItem);
                    }
                }

                cartDetails.Add(cartDetail);
            }

            return cartDetails;
        }

        public async Task<Cart> GetCart(string id)
        {
            var builder = new UriBuilder(ApiEndpoints.BaseApiUrl)
            {
                Path = string.Format(ApiEndpoints.CartEndpoint, id)
            };
            var result = await genericService.GetAsync<ActionResponse<Cart>>(builder.ToString());

            return !result.WasSuccessful ? new Cart() : result.Value;
        }

        public async Task<IEnumerable<CartItem>> GetCartItems(string id)
        {
            var builder = new UriBuilder(ApiEndpoints.BaseApiUrl)
            {
                Path = string.Format(ApiEndpoints.CartItemsEndpoint, id)
            };

            var result = await genericService.GetAsync<ActionResponse<IEnumerable<CartItem>>>(builder.ToString());

            return result.WasSuccessful ? result.Value : new List<CartItem>();
        }

        public async Task<Cart> CreateCart(string customerId, string cartName)
        {
            var builder = new UriBuilder(ApiEndpoints.BaseApiUrl)
            {
                Path = $"{ApiEndpoints.CreateCartEndpoint}"
            };
            dynamic data = new JObject();
            data.CustomerId = customerId;
            data.CartName = cartName;

            var result = await genericService.PostAsync<dynamic, ActionResponse<string>>(builder.ToString(), data);

            return result.WasSuccessful ? await GetCart(result.Value) : new Cart();
        }

        public async Task AddToCart(string cartId, string productId, int quantity)
        {
            var builder = new UriBuilder(ApiEndpoints.BaseApiUrl)
            {
                Path = string.Format(ApiEndpoints.CartItemsEndpoint, cartId)
            };

            dynamic data = new JObject();
            data.CartId = cartId;
            data.ProductId = productId;
            data.Quantity = quantity;

            await genericService.PostAsync<dynamic, ActionResponse>(builder.ToString(), data);
        }

        public async Task ChangeQuantity(string cartId, string productId, int quantity)
        {
            var builder = new UriBuilder(ApiEndpoints.BaseApiUrl)
            {
                Path = string.Format(ApiEndpoints.CartEndpoint, cartId)
            };

            dynamic data = new JObject();
            data.CartId = cartId;
            data.ProductId = productId;
            data.Quantity = quantity;

            await genericService.PutAsync<dynamic, ActionResponse>(builder.ToString(), data);
        }
    }

    public interface ICartService
    {
        Task<IEnumerable<CartDetail>> GetCarts();
        Task<Cart> GetCart(string id);
        Task<IEnumerable<CartItem>> GetCartItems(string id);
        Task<Cart> CreateCart(string customerId, string cartName);
        Task AddToCart(string cartId, string productId, int quantity);
        Task ChangeQuantity(string cartId, string productId, int quantity);
    }
}
