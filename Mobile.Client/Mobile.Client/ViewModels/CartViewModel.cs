using System.Collections.ObjectModel;
using System.Windows.Input;
using Mobile.Client.Behaviors;
using Mobile.Client.Extensions;
using Mobile.Client.Models;
using Mobile.Client.Services;
using Xamarin.Forms;

namespace Mobile.Client.ViewModels
{
    public class CartViewModel : ViewModelBase<Cart>
    {
        private readonly INavigator navigation;
        private readonly ICartService cartService;
        private readonly IAnimate animate;
        public ICommand ReturnToHomeCommand => new Command(OnReturnToHome);
        public ICommand AddProductCommand => new Command(OnAddProduct);
        public ICommand SelectedItemCommand => new Command<CartItem>(OnSelectedItem);
        public ICommand UpdateItemCommand => new Command(OnUpdateItem);
        public ICommand ToggleDialogCommand => new Command<Frame>(f =>
        {
            ShowCloseDialog = !ShowCloseDialog;
            CanReturnToHome = !ShowCloseDialog;
        });
        public ICommand DialogStateChangedCommand => new Command<TargetPropertyChanged>(OnDialogStateChangedCommand);
        private CartItem selectedItem;

        public CartItem SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

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

        private ObservableCollection<CartItem> selectedProducts;

        public ObservableCollection<CartItem> SelectedProducts
        {
            get => selectedProducts;
            private set
            {
                selectedProducts = value;
                OnPropertyChanged(nameof(SelectedProducts));
            }
        }

        private bool isEmpty;
        public bool IsEmpty
        {
            get => isEmpty;
            set
            {
                isEmpty = value;
                OnPropertyChanged(nameof(IsEmpty));
            }
        }

        private bool showCloseDialog;
        public bool ShowCloseDialog
        {
            get => showCloseDialog;
            private set
            {
                showCloseDialog = value;
                OnPropertyChanged(nameof(ShowCloseDialog));
            }
        }

        private bool canReturnToHome;
        public bool CanReturnToHome
        {
            get => canReturnToHome;
            set
            {
                canReturnToHome = value;
                OnPropertyChanged(nameof(CanReturnToHome));
            }
        }

        public CartViewModel(INavigator navigation, 
            ICartService cartService, IAnimate animate)
        {
            this.navigation = navigation;
            this.cartService = cartService;
            this.animate = animate;
        }

        public override async void InitializeDataAsync(Cart data)
        {
            Cart = data;
            ShowCloseDialog = false;
            CanReturnToHome = true;
            SelectedProducts = (await cartService.GetCartItems(data.Id)).ToObservableCollection();
            IsEmpty = SelectedProducts.Count == 0;
        }

        public async void OnReturnToHome()
        {
            await navigation.PushAsync<CartSummaryViewModel>();
        }

        public void OnAddProduct()
        {
            navigation.PushAsync<CartItemSelectionViewModel, Cart>(Cart);
        }

        public void OnSelectedItem(CartItem cartItem)
        {
            var x = SelectedItem;
            SelectedItem = cartItem;
        }

        public async void OnUpdateItem()
        {
            await cartService.ChangeQuantity(Cart.Id, SelectedItem.ProductId, SelectedItem.Quantity);
            ShowCloseDialog = false;
            CanReturnToHome = true;
        }

        public void OnDialogStateChangedCommand(TargetPropertyChanged args)
        {
            animate.Toggle(args);
        }
    }
}
