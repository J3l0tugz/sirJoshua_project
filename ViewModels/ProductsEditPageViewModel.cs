using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Mamilots_POS.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Mamilots_POS.ViewModels
{
    partial class ProductsEditPageViewModel : ViewModelBase
    {
        public ObservableCollection<Product> Products { get; set; }

        public ProductsEditPageViewModel()
        {
            Products = new ObservableCollection<Product>
            {
                new Product { Id = 1, Name = "Original Taro Chips", IsBestSeller = 0, CategoryId = 0, Price = 120.00f },
                new Product { Id = 2, Name = "Sweet Banana Chips", IsBestSeller = 1, CategoryId = 1, Price = 120.00f },
                new Product { Id = 3, Name = "Sweet Camote Chips", IsBestSeller = 1, CategoryId = 2, Price = 109.99f }
            };
        }

        [RelayCommand]
        public void Add() { /* Add logic here */ }

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
