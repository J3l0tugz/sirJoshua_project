using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using Mamilots_POS.Helpers;
using Mamilots_POS.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;



namespace Mamilots_POS.ViewModels
{
    internal partial class ProductsContentViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private ObservableCollection<Item> _items;

        public ObservableCollection<Item> Items
        {
            get => _items;
            set
            {
                if (_items != value)
                {
                    _items = value;
                    OnProductPropertyChanged();
                }
            }
        }

        public ProductsContentViewModel(AuthenticationResult authResult)
        {

        }

        // Constructor to set the ItemNames
        public ProductsContentViewModel()
        {
            Items = new ObservableCollection<Item>
    {
            new Item { Name = "Original Taro Chips", ImagePath = ImageHelper.LoadFromResource("avares://Mamilots_POS/Assets/Images/taroChips.png"), },
            new Item { Name = "Sweet Banana Chips", ImagePath = ImageHelper.LoadFromResource("avares://Mamilots_POS/Assets/Images/taroChips.png"), }

    };
        }

        public event PropertyChangedEventHandler ProductPropertyChanged;

        protected void OnProductPropertyChanged([CallerMemberName] string propertyName = null)
        {
            ProductPropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }

    public class Item
    {

        public string? Name { get; set; }
        public Bitmap? ImagePath { get; set; }
    }

}
