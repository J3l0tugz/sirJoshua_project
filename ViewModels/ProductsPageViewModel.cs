using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using Mamilots_POS.Helpers;
using Mamilots_POS.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;



namespace Mamilots_POS.ViewModels
{
    internal partial class ProductsPageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private ObservableCollection<Category> _categories;

        

        [ObservableProperty] private string? _greeting = "ddsfsdfsdfsdfsdfsdf";
        private string? _greetings => "ddsfsdfsdfsdfsdfsdf";

        public ObservableCollection<Category> Categories
        {
            get => _categories;
            set
            {
                if (_categories != value)
                {
                    _categories = value;
                    OnPropertyChanged();
                }
            }
        }

        public ProductsPageViewModel(AuthenticationResult authResult)
        {

        }

        // Constructor to set the CategoryNames
        public ProductsPageViewModel()
        {
            Categories = new ObservableCollection<Category>
    {
        new Category { Name = "All Products", ImagePath = ImageHelper.LoadFromResource("avares://Mamilots_POS/Assets/Images/taroChips.png") },
        new Category { Name = "Best Sellers", ImagePath = ImageHelper.LoadFromResource("avares://Mamilots_POS/Assets/Images/taroChips.png") },
        new Category { Name = "Taro Chips", ImagePath = ImageHelper.LoadFromResource("avares://Mamilots_POS/Assets/Images/taroChips.png") },
        new Category { Name = "Banana Chips", ImagePath = ImageHelper.LoadFromResource("avares://Mamilots_POS/Assets/Images/taroChips.png") },
        new Category { Name = "Camote Chips", ImagePath = ImageHelper.LoadFromResource("avares://Mamilots_POS/Assets/Images/taroChips.png") },
        new Category { Name = "Other Products", ImagePath = ImageHelper.LoadFromResource("avares://Mamilots_POS/Assets/Images/taroChips.png") },
    };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }

    public class Category 
    {
        
        public string? Name { get; set; }
        public Bitmap? ImagePath { get; set; }
    }

}
