using BusinessObject.Models;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp.ViewModel
{
    public class ProductViewModel : ViewModelBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISupplierRepository _supplierRepository;

        private FlowerBouquet _product;

        private FlowerBouquet _selectedItem;
        private Category _selectedItemCategory;
        private Supplier _selectedItemSupplier;
        private string _message;
        private string _search;
        private ObservableCollection<FlowerBouquet> _flowerBouquets;
        private ObservableCollection<Category> _categories;
        private ObservableCollection<Supplier> _suppliers;

        public RelayCommand AddCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }
        public ObservableCollection<FlowerBouquet> FlowerBouquets
        {
            get => _flowerBouquets;
            set
            {
                _flowerBouquets = value; OnPropertyChanged();
            }
        }
        public FlowerBouquet SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); }
        }

        public string Message
        {
            get => _message;
            set { _message = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Category> Categories { get => _categories; set { _categories = value; OnPropertyChanged(); } }
        public ObservableCollection<Supplier> Suppliers { get => _suppliers; set { _suppliers = value; OnPropertyChanged(); } }

        public Category SelectedItemCategory { get => _selectedItemCategory; set { _selectedItemCategory = value; OnPropertyChanged(); } }

        public Supplier SelectedItemSupplier { get => _selectedItemSupplier; set { _selectedItemSupplier = value; OnPropertyChanged(); } }

        public FlowerBouquet Product { get => _product; set { _product = value; OnPropertyChanged(); } }

        public string Search { get => _search; set { _search = value; OnPropertyChanged(); FlowerBouquets = SearchProduct(); } }

        public ProductViewModel(IProductRepository productRepository, ICategoryRepository categoryRepository, ISupplierRepository supplierRepository)
        {
            Product = new FlowerBouquet();
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
            _productRepository = productRepository;
            DeleteCommand = new RelayCommand(execute => DeleteProduct(), canExcute => CanDeleteProduct());
            AddCommand = new RelayCommand(AddProduct, CanAddProduct);
            UpdateCommand = new RelayCommand(UpdateProduct, CanUpdateProduct);
            FlowerBouquets = GetAllProduct();
            Categories = GetAllCategory();
            Suppliers = GetAllSupplier();
            SelectedItemCategory = Categories.First();
            SelectedItemSupplier = Suppliers.First();

        }

        private async void UpdateProduct(object? obj)
        {
            _productRepository.Update(SelectedItem);
            await ShowMessageAsync("Update succesfully", 1);
        }

        private bool CanUpdateProduct(object? arg)
        {
            return SelectedItem != null;
        }

        private bool CanAddProduct(object? arg)
        {

            if (Product.FlowerBouquetId == 0 || _productRepository.GetById(Product.FlowerBouquetId) != null)
            {
                return false;
            }

            return true;
        }

        private async void AddProduct(object? obj)
        {
            Product.FlowerBouquetStatus = 1;
            Product.Category = SelectedItemCategory;
            Product.Supplier = SelectedItemSupplier;
            _productRepository.Add(Product);
            Product = new FlowerBouquet();
            FlowerBouquets = GetAllProduct();
            SelectedItemCategory = Categories.First();
            SelectedItemSupplier = Suppliers.First();
            await ShowMessageAsync("Add product successfully", 1);
        }

        private bool CanDeleteProduct()
        {
            return SelectedItem != null;
        }

        private async void DeleteProduct()
        {
            var comfirm = MessageBox.Show("Do you want remove?", "Confirm remove", MessageBoxButton.YesNo);
            if (comfirm == MessageBoxResult.Yes)
            {
                _productRepository.Delete(SelectedItem.FlowerBouquetId);
                FlowerBouquets = GetAllProduct();
                await ShowMessageAsync("Delete successfully", 1);
            }
        }

        private ObservableCollection<FlowerBouquet> GetAllProduct()
        {
            return new ObservableCollection<FlowerBouquet>(_productRepository.GetAll());
        }
        private ObservableCollection<Category> GetAllCategory()
        {
            return new ObservableCollection<Category>(_categoryRepository.GetAll());
        }
        private ObservableCollection<Supplier> GetAllSupplier()
        {
            return new ObservableCollection<Supplier>(_supplierRepository.GetAll());
        }

        private ObservableCollection<FlowerBouquet> SearchProduct()
        {
            return new ObservableCollection<FlowerBouquet>(_productRepository.GetAll().Where(p => p.FlowerBouquetName.Contains(Search, StringComparison.OrdinalIgnoreCase)));
        }

        public async Task ShowMessageAsync(string message, int durationInSeconds)
        {
            Message = message;
            await Task.Delay(durationInSeconds * 1000);
            Message = string.Empty;
        }
    }
}
