using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Mamilots_POS.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Data.SqlTypes;
using System.Diagnostics;
using System;
using Mamilots_POS.Repositories;
using Mamilots_POS.Services;

namespace Mamilots_POS.ViewModels
{
    partial class ProductsEditPageViewModel : ViewModelBase
    {
        private ObservableCollection<Product> _products = new ObservableCollection<Product>();
        public ObservableCollection<Product> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }

        [ObservableProperty]
        private string? _productName;

        [ObservableProperty]
        private string? _productPrice;

        [ObservableProperty]
        private int _productCategory;

        [ObservableProperty]
        private bool _isBestSeller;

        [ObservableProperty]
        private string? _errorMessage;

        private readonly IAddProductService _addProductService;
        private readonly IProductRepository _productRepository;

        public ProductsEditPageViewModel()
        {
            _addProductService = new AddProductService();
            _productRepository = new ProductRepository();
            LoadProducts();

        }

        private async void LoadProducts()
        {
            int i = 0;
            Debug.WriteLine(_productRepository.GetProductsAsync().ToString());
            await foreach (var product in _productRepository.GetProductsAsync())
            {
                Debug.WriteLine(product.Name);
                Debug.WriteLine(product.IsBestSeller);
                Debug.WriteLine(product.Category.Id);
                Debug.WriteLine(product.Price);
                Debug.WriteLine("INDEX: " + i++);
                Debug.WriteLine("-----------------");
                if (product != null)
                {
                    Products.Add(new Product
                    {
                        Name = product.Name,
                        IsBestSeller = product.IsBestSeller,
                        CategoryId = product.Category.Id,
                        Price = product.Price
                    });
                }


            }
        }

        [RelayCommand]
        public void Add() 
        {
            try
            {
                if(ProductName != null || ProductPrice != null|| ProductCategory != null)
                {
                    _addProductService.AddProduct(
                        ProductName,
                        IsBestSeller,
                        ProductCategory,
                        (SqlMoney) float.Parse(ProductPrice)
                        );
                    LoadProducts();
                }else
                {
                    ErrorMessage = "Insert error message";
                }

            }
            catch (FormatException e)
            {
                ErrorMessage = "Price may not contain letters";
            }
            catch(ArgumentNullException e)
            {
                ErrorMessage = "Price can't be null";
            }
        }

        [RelayCommand]
        public void Edit() { /* Edit logic here */ }

        [RelayCommand]
        public void Delete() { /* Delete logic here */ }



        [ObservableProperty]
        private bool isAddModalVisible;

        [RelayCommand]
        public void ShowAddModal()
        {
            IsAddModalVisible = true;
        }

        [RelayCommand]
        public void HideAddModal()
        {
            IsAddModalVisible = false;
        }




        [ObservableProperty]
        private bool isEditModalVisible;

        [RelayCommand]
        public void ShowEditModal()
        {
            IsEditModalVisible = true;
        }

        [RelayCommand]
        public void HideEditModal()
        {
            IsEditModalVisible = false;
        }



        [ObservableProperty]
        private bool isDeleteModalVisible;

        [RelayCommand]
        public void ShowDeleteModal()
        {
            IsDeleteModalVisible = true;
        }

        [RelayCommand]
        public void HideDeleteModal()
        {
            IsDeleteModalVisible = false;
        }
    }
}
