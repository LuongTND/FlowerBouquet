using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp.View;

namespace WpfApp.ViewModel
{
    class NavigationVM : ViewModelBase
    {
        private CustomerViewModel _customerViewModel;
        private OrderViewModel _orderViewModel;
        private ProductViewModel _productViewModel;
        private HomeViewModel _homeViewModel;

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); CustomerViewModel.Message = null; }
        }

        public ICommand CustomerCommand { get; set; }
        public ICommand OrderCommand { get; set; }
        public ICommand ProductCommand { get; set; }
        public ICommand HomeCommand { get; set; }

        public CustomerViewModel CustomerViewModel
        {
            get { return _customerViewModel; }
            set { _customerViewModel = value; OnPropertyChanged(); }
        }

        public OrderViewModel OrderViewModel
        {
            get { return _orderViewModel; }
            set { _orderViewModel = value; OnPropertyChanged(); }
        }

        public ProductViewModel ProductViewModel
        {
            get => _productViewModel;
            set
            {
                _productViewModel = value; OnPropertyChanged();
            }
        }

        public HomeViewModel HomeViewModel { get => _homeViewModel; set { _homeViewModel = value; OnPropertyChanged(); } }

        public NavigationVM(CustomerViewModel customerViewModel, OrderViewModel orderViewModel, ProductViewModel productViewModel, HomeViewModel homeViewModel)
        {
            _customerViewModel = customerViewModel;
            _orderViewModel = orderViewModel;
            _productViewModel = productViewModel;
            _homeViewModel = homeViewModel;
            CustomerCommand = new RelayCommand(excute => CurrentView = _customerViewModel);
            OrderCommand = new RelayCommand(excute => CurrentView = _orderViewModel);
            ProductCommand = new RelayCommand(excute => CurrentView = _productViewModel);
            HomeCommand = new RelayCommand(excute => CurrentView = _homeViewModel);
            // Startup Page
            CurrentView = _homeViewModel;

        }
    }
}
