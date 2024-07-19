using BusinessObject.Models;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp.View;
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp.ViewModel
{
    public class CustomerViewModel : ViewModelBase
    {

        private readonly ICustomerRepository _customerRepository;
        private string _message;
        private Customer _customer;
        private Customer _selectedItem;
        private string _search;
        private ObservableCollection<Customer> _customers;

        public string Password { private get; set; }

        public RelayCommand UpdateProfileCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        public Customer SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); }
        }

        public Customer Customer
        {
            get { return _customer; }
            set { _customer = value; OnPropertyChanged(); }
        }

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

        public string Search { get => _search; set { _search = value; OnPropertyChanged(); Customers = SearchCustomer(); } }

        public CustomerViewModel(ICustomerRepository customerRepository)
        {
            _customer = new Customer();
            _customerRepository = customerRepository;
            UpdateProfileCommand = new RelayCommand(execute => UpdateProfile());
            DeleteCommand = new RelayCommand(execute => DeleteCustomer(), canExcute => CanDeleteCustomer());
            AddCommand = new RelayCommand(AddCustomer, CanAddCustomer);
            UpdateCommand = new RelayCommand(UpdateCustomer, CanUpdateCustomer);
            Customers = GetAll();
        }

        private bool CanUpdateCustomer(object? arg)
        {

            return SelectedItem != null;
        }

        private async void UpdateCustomer(object? obj)
        {
            _customerRepository.Update(SelectedItem);
            await ShowMessageAsync("Update succesfully", 1);
        }
        private bool CanDeleteCustomer()
        {
            return SelectedItem != null;
        }

        private async void DeleteCustomer()
        {
            var comfirm = MessageBox.Show("Do you want remove?", "Confirm remove", MessageBoxButton.YesNo);
            if (comfirm == MessageBoxResult.Yes)
            {
                _customerRepository.Delete(SelectedItem.CustomerId);
                Customers = GetAll();
                await ShowMessageAsync("Delete customer successfully", 1);
            }

        }

        private bool CanAddCustomer(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            Customer.Password = passwordBox.Password;
            if (string.IsNullOrWhiteSpace(Customer.Email) || string.IsNullOrWhiteSpace(Customer.Password) || Customer.CustomerId == 0 || _customerRepository.GetById(Customer.CustomerId) != null)
            {
                return false;
            }

            return true;
        }

        private async void AddCustomer(object parameter)
        {
            _customerRepository.Add(Customer);
            Customer = new Customer();
            Customers = GetAll();
            var passwordBox = parameter as PasswordBox;
            passwordBox.Password = string.Empty;
            await ShowMessageAsync("Add customer successfully", 1);
        }

        private async void UpdateProfile()
        {
            _customerRepository.Update(Customer);
            await ShowMessageAsync("Profile updated successfully", 1);

        }

        private ObservableCollection<Customer> GetAll()
        {
            return new ObservableCollection<Customer>(_customerRepository.GetAll());
        }
        public async Task ShowMessageAsync(string message, int durationInSeconds)
        {
            Message = message;
            await Task.Delay(durationInSeconds * 1000);
            Message = string.Empty;
        }

        private ObservableCollection<Customer> SearchCustomer()
        {
            return new ObservableCollection<Customer>(_customerRepository.GetAll().Where(p => p.CustomerName.Contains(Search, StringComparison.OrdinalIgnoreCase)));
        }
    }
}
