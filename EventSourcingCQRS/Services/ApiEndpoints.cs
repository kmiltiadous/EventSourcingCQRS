namespace EventSourcingCQRS.Services
{
    public class ApiEndpoints
    {
        public const string BaseApiUrl = "https://localhost:44363/";
        public const string CartsEndpoint = "api/carts";
        public const string CartEndpoint = "api/carts/{0}";
        public const string CartItemsEndpoint = "api/carts/{0}/items";
        public const string CreateCartEndpoint = "api/carts";
        public const string CustomersEndpoint = "api/customers";
        public const string ProductsEndpoint = "api/products";
    }
}
