using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp.ViewModel
{
    public class OrderViewModel : ViewModelBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private Customer _customer;
        private Order _order;
        private string _message;
        private ObservableCollection<Customer> _customers;
        private Customer _selectedItemCustomer;
        private string _search;

        public string Message
        {
            get => _message;
            set { _message = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set
            {
                _customers = value; OnPropertyChanged();
            }
        }
        public string Search { get => _search; set { _search = value; OnPropertyChanged(); Orders = SearchOrder(); } }

        private ObservableCollection<Order> SearchOrder()
        {
            return new ObservableCollection<Order>(_orderRepository.GetAll().Where(p => p.Customer.CustomerName.Contains(Search, StringComparison.OrdinalIgnoreCase)));

        }

        public Customer Customer
        {
            get { return _customer; }
            set
            {
                _customer = value; OnPropertyChanged();
                Orders = new ObservableCollection<Order>(Customer != null ? _orderRepository.GetAll().Where(o => o.CustomerId == Customer.CustomerId) : new List<Order>());
            }
        }

        private Order _selectedItem;
        public Order SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders { get => _orders; set { _orders = value; OnPropertyChanged(); } }

        public Customer SelectedItemCustomer { get => _selectedItemCustomer; set { _selectedItemCustomer = value; OnPropertyChanged(); } }

        public RelayCommand AddCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }
        public Order Order { get => _order; set { _order = value; OnPropertyChanged(); } }

        public OrderViewModel(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            Order = new Order();
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            DeleteCommand = new RelayCommand(execute => DeleteOrder(), canExcute => CanDeleteOrder());
            AddCommand = new RelayCommand(AddOrder, CanAddOrder);
            UpdateCommand = new RelayCommand(UpdateOrder, CanUpdateOrder);
            Customers = GetAllCustomer();
            if (!Customers.IsNullOrEmpty())
            {
                SelectedItemCustomer = Customers.First();
            }

            Orders = GetAllOrder();

        }

        private bool CanAddOrder(object? arg)
        {
            if (Order.OrderId == 0 || _orderRepository.GetById(Order.OrderId) != null)
            {
                return false;
            }

            return true;
        }

        private async void AddOrder(object? obj)
        {

            Order.Customer = SelectedItemCustomer;
            _orderRepository.Add(Order);
            Order = new Order();
            Orders = GetAllOrder();
            SelectedItemCustomer = Customers.First();
            await ShowMessageAsync("Add successfully", 1);
        }

        private bool CanUpdateOrder(object? arg)
        {
            return SelectedItem != null;
        }

        private async void UpdateOrder(object? obj)
        {
            _orderRepository.Update(SelectedItem);
            await ShowMessageAsync("Update succesfully", 1);
        }

        private bool CanDeleteOrder()
        {
            return SelectedItem != null;
        }

        private async void DeleteOrder()
        {
            var comfirm = MessageBox.Show("Do you want remove?", "Confirm remove", MessageBoxButton.YesNo);
            if (comfirm == MessageBoxResult.Yes)
            {
                _orderRepository.Delete(SelectedItem.OrderId);
                Orders = GetAllOrder();
                await ShowMessageAsync("Delete successfully", 1);
            }
        }

        private ObservableCollection<Customer> GetAllCustomer()
        {
            return new ObservableCollection<Customer>(_customerRepository.GetAll());
        }

        private ObservableCollection<Order> GetAllOrder()
        {
            return new ObservableCollection<Order>(_orderRepository.GetAll());
        }

        public async Task ShowMessageAsync(string message, int durationInSeconds)
        {
            Message = message;
            await Task.Delay(durationInSeconds * 1000);
            Message = string.Empty;
        }
    }
}
