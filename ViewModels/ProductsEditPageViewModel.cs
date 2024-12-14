using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Mamilots_POS.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Data.SqlTypes;
using System.Diagnostics;
using System;
using Mamilots_POS.Repositories;
using Mamilots_POS.Services;
using DynamicData;
using System.Linq;
using FluentAvalonia.Core;

namespace Mamilots_POS.ViewModels
{
    partial class ProductsEditPageViewModel : ViewModelBase
    {
        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }

        [ObservableProperty]
        private Product _currProduct;

        [ObservableProperty]
        private string? _currProductPrice;

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
        private readonly IDeleteProductService _deleteProductService;
        private readonly IEditProductService _editProductService;
        private readonly IProductRepository _productRepository;

        public ProductsEditPageViewModel()
        {
            _addProductService = new AddProductService();
            _productRepository = new ProductRepository();
            _deleteProductService = new DeleteProductService();
            _editProductService = new EditProductService();
            LoadProducts();

        }

        private async void LoadProducts()
        {
            _products = new ObservableCollection<Product>();
            await foreach (var product in _productRepository.GetProductsAsync())
            {
                if (product != null)
                {
                    Products.Add(new Product
                    {
                        Name = product.Name,
                        IsBestSeller = product.IsBestSeller,
                        CategoryId = product.CategoryId,
                        Price = product.Price,
                        Id = product.Id
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
                    Products.Add(new Product
                    {
                        Name = _productName,
                        IsBestSeller = _isBestSeller,
                        CategoryId = _productCategory,
                        Price = (SqlMoney)float.Parse(_productPrice),
                        Id = _productRepository.GetHighestId()

                    });
                    HideAddModal();
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
        public void Edit(Product product)
        {
            _editProductService.UpdateProduct(product.Id, product.Name, product.IsBestSeller, product.CategoryId, SqlMoney.Parse(CurrProductPrice));
            Product temp = Products.First(x => x.Id == CurrProduct.Id);
            if (temp != null)
            {
                product.Price = SqlMoney.Parse(CurrProductPrice);
                Products.Replace(temp, product);
            }
            HideEditModal();
        }

        [RelayCommand]
        public void Delete(int Id) 
        {
            Debug.WriteLine(Id);
            _deleteProductService.DeleteProduct(Id);
            Product temp = Products.First(x => x.Id == Id);
            if (temp != null)
            {
                Debug.WriteLine(temp.Id);
                Products.Remove(temp);
            }
            HideDeleteModal();
        }



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
        public void ShowEditModal(int Id)
        {
            CurrProduct = _productRepository.GetProduct(Id);
            CurrProductPrice = CurrProduct.Price + "";
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
        public void ShowDeleteModal(int Id)
        {
            CurrProduct = _productRepository.GetProduct(Id);
            IsDeleteModalVisible = true;
        }

        [RelayCommand]
        public void HideDeleteModal()
        {
            IsDeleteModalVisible = false;
        }
    }
}
