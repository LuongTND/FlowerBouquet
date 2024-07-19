using BusinessObject.Models;
using DataAccess.Repository;
using DataAccess.Repository.Impl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApp.UserControls;
using WpfApp.View;
using WpfApp.ViewModel;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;
        public App()
        {
            ServiceCollection services = new ServiceCollection();
            // repository
            services.AddSingleton(typeof(ICustomerRepository), typeof(CustomerRepositoryImpl));
            services.AddSingleton(typeof(IOrderRepository), typeof(OrderRepositoryImpl));
            services.AddSingleton(typeof(ISupplierRepository), typeof(SupplierRepositoryImpl));
            services.AddSingleton(typeof(ICategoryRepository), typeof(CategoryRepositoryImpl));
            services.AddSingleton(typeof(IOrderDetailRepository), typeof(OrderDetailRepositoryImpl));
            services.AddSingleton(typeof(IProductRepository), typeof(ProductRepositoryImpl));

            // windown
            services.AddSingleton<MainWindow>();
            services.AddSingleton<LoginWindown>(provider => new LoginWindown
            {
                DataContext = provider.GetRequiredService<LoginViewModel>()
            });
            services.AddSingleton<AdminWindown>(provider => new AdminWindown
            {
                DataContext = provider.GetRequiredService<NavigationVM>()
            });
            services.AddSingleton<CustomerWindown>(provider => new CustomerWindown
            {
                DataContext = provider.GetRequiredService<NavigationVM>()
            });

            // view model
            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<NavigationVM>();
            services.AddSingleton<CustomerViewModel>();
            services.AddSingleton<OrderViewModel>();
            services.AddSingleton<ProductViewModel>();
            services.AddSingleton<HomeViewModel>();

            // context
            services.AddSingleton<FuflowerBouquetManagementContext>();
            serviceProvider = services.BuildServiceProvider();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var loginWindown = serviceProvider.GetService<LoginWindown>();
            loginWindown.Show();
        }
    }
}
