using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp.UserControls;
using WpfApp.View;

namespace WpfApp.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IConfiguration _configuration;
        private readonly CustomerWindown _customerWindown;
        private readonly AdminWindown _adminWindown;
        private string _username;

        private string _message;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                Message = null;
                OnPropertyChanged();
            }
        }

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand LoginCommand { get; set; }

        public LoginViewModel(ICustomerRepository customerRepository, CustomerWindown customerWindown, AdminWindown adminWindown)
        {
            _customerWindown = customerWindown;
            _adminWindown = adminWindown;
            _customerRepository = customerRepository;
            _configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();

            LoginCommand = new RelayCommand(execute => Login(execute));

        }

        private void Login(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var email = _configuration["DefaultAccount:Email"];
            var password = _configuration["DefaultAccount:Password"];

            if (Username.Equals(email) && passwordBox.Password.Equals(password))
            {
                var loginWindow = Application.Current.Windows.OfType<LoginWindown>().FirstOrDefault();
                loginWindow?.Close();
                NavigationVM navigationVM = (_adminWindown.DataContext as NavigationVM);
                navigationVM.HomeViewModel.Customer = new Customer { Email = email };
                _adminWindown.Show();
                return;
            }
            var customer = _customerRepository.SignIn(Username, passwordBox.Password);

            if (customer != null)
            {
                var loginWindow = Application.Current.Windows.OfType<LoginWindown>().FirstOrDefault();
                loginWindow?.Close();
                NavigationVM navigationVM = (_customerWindown.DataContext as NavigationVM);
                navigationVM.CustomerViewModel.Customer = customer;
                navigationVM.OrderViewModel.Customer = customer;
                navigationVM.HomeViewModel.Customer = customer;
                _customerWindown.Show();
                return;
            }
            else
            {
                Message = "Username or password is valid!";
            }

        }
    }
}