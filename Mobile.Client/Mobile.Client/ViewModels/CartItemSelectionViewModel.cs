using System.Collections.ObjectModel;
using System.Windows.Input;
using Mobile.Client.Extensions;
using Mobile.Client.Models;
using Mobile.Client.Services;
using Xamarin.Forms;

namespace Mobile.Client.ViewModels
{
    public class CartItemSelectionViewModel : ViewModelBase<Cart>
    {
        private readonly INavigator navigation;
        private readonly IProductService productService;
        private readonly ICartService cartService;
        public ICommand AddProductCommand => new Command(OnAddProduct);
        public ICommand ReturnToCartCommand => new Command(OnReturnToCart);
        public ICommand SelectedProductCommand => new Command<Product>(OnSelectedProduct);

        public Product SelectedProduct { get; set; }
        private Cart cart;
        public Cart Cart
        {
            get => cart;
            private set
            {
                cart = value;
                OnPropertyChanged(nameof(Cart));
            }
        }

        private ObservableCollection<Product> products;
        public ObservableCollection<Product> Products
        {
            get => products;
            set
            {
                products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        private string selectedProductDisplayName;

        public string SelectedProductDisplayName
        {
            get => selectedProductDisplayName;
            set
            {
                selectedProductDisplayName = value;
                OnPropertyChanged(nameof(SelectedProductDisplayName));
            }
        }

        private int selectedQuantity;

        public int SelectedQuantity
        {
            get => selectedQuantity;
            set
            {
                selectedQuantity = value;
                OnPropertyChanged(nameof(SelectedQuantity));
            }
        }

        public CartItemSelectionViewModel(INavigator navigation, 
            IProductService productService, ICartService cartService)
        {
            this.navigation = navigation;
            this.productService = productService;
            this.cartService = cartService;
            SelectedProductDisplayName = "Select from list";
            SelectedQuantity = 0;

        }

        public override async void InitializeDataAsync(Cart data)
        {
            Cart = data;
            Products = (await productService.GetProducts()).ToObservableCollection();
        }

        public void OnSelectedProduct(Product product)
        {
            SelectedProduct = product;
            SelectedProductDisplayName = product.Name;
            SelectedQuantity = 1;
        }

        public void OnReturnToCart()
        {
            navigation.PushAsync<CartViewModel, Cart>(Cart);
        }

        public async void OnAddProduct()
        {
            if (SelectedProduct == null) return;

            await cartService.AddToCart(Cart.Id, SelectedProduct.Id, SelectedQuantity);
            await navigation.PushAsync<CartViewModel, Cart>(Cart);
        }
    }
}
