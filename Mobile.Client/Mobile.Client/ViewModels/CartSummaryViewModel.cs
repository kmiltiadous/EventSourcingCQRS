using System.Collections.ObjectModel;
using System.Windows.Input;
using Mobile.Client.Behaviors;
using Mobile.Client.Extensions;
using Mobile.Client.Models;
using Mobile.Client.Services;
using Xamarin.Forms;

namespace Mobile.Client.ViewModels
{
    public class CartSummaryViewModel : ViewModelBase
    {
        private readonly INavigator navigation;
        private readonly ICartService cartService;
        private readonly ICustomerService customerService;
        private readonly IAnimate animate;
        public ICommand CreateCartCommand => new Command(OnCreateCart);
        public ICommand ToggleDialogCommand => new Command<Frame>(f =>
        {
            ShowCloseDialog = !ShowCloseDialog;
        });

        public ICommand SelectCartCommand => new Command<Cart>(OnSelectCart);
        public ICommand DialogStateChangedCommand => new Command<TargetPropertyChanged>(OnDialogStateChangedCommand);
        private ObservableCollection<CartDetail> groups;
        public ObservableCollection<CartDetail> Groups
        {
            get => groups;
            private set
            {
                groups = value;
                OnPropertyChanged(nameof(Groups));
            }
        }

        private ObservableCollection<Customer> customers;
        public ObservableCollection<Customer> Customers
        {
            get => customers;
            private set
            {
                customers = value;
                OnPropertyChanged(nameof(Customers));
            }
        }

        private Customer selectedCustomer;
        public Customer SelectedCustomer
        {
            get => selectedCustomer;
            set
            {
                selectedCustomer = value;
                OnPropertyChanged(nameof(SelectedCustomer));
            }
        }

        private string newCartName;
        public string NewCartName
        {
            get => newCartName;
            set
            {
                newCartName = value;
                OnPropertyChanged(nameof(NewCartName));
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

        public CartSummaryViewModel(INavigator navigation, 
            ICartService cartService, 
            ICustomerService customerService,
            IAnimate animate)
        {
            this.navigation = navigation;
            this.cartService = cartService;
            this.customerService = customerService;
            this.animate = animate;
        }

        public override async void InitializeDataAsync()
        {
            Groups = (await cartService.GetCarts()).ToObservableCollection();
            Customers = (await customerService.GetCustomers()).ToObservableCollection();
            IsEmpty = Groups.Count == 0;
        }

        public async void OnCreateCart()
        {
            var result = await cartService.CreateCart(SelectedCustomer.Id, NewCartName);
            SelectedCustomer = null;
            NewCartName = null;
            ShowCloseDialog = false;
            await navigation.PushAsync<CartViewModel, Cart>(result);
        }

        public async void OnSelectCart(Cart cart)
        {
            await navigation.PushAsync<CartViewModel, Cart>(cart);
        }

        public void OnDialogStateChangedCommand(TargetPropertyChanged args)
        {
            animate.Toggle(args);
        }
    }
}

