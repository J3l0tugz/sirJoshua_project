using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Mamilots_POS.Messages;
using Mamilots_POS.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace Mamilots_POS.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private ListItemTemplate _historyItem;

        public MainWindowViewModel() : this(MessengerInstance.Instance) { }

        public MainWindowViewModel(IMessenger messenger)
        {
            _historyItem = new ListItemTemplate(typeof(HistoryPageViewModel), "historyregular");

            messenger.Register<MainWindowViewModel, LoginSuccessMessage>(this, (recipient, message) =>
            {
                CurrentPage = new ProductsPageViewModel(message.Value);
                Trace.WriteLine("LoginSuccessMessage received with value: " + message.Value);

                LabelLog = "Logout";
                BtnColor = new SolidColorBrush(Colors.Red);
                LblColor = new SolidColorBrush(Colors.White);
                IsPaneOpen = true;
                PaneLength = 65;

                // Set the selected item to ProductsPageViewModel after login
                SelectedListItem = Items.FirstOrDefault(item => item.ModelType == typeof(ProductsPageViewModel));

                // Add HistoryPageViewModel to the list
                Items.Add(_historyItem);
            });

            IsPaneOpen = false;

            messenger.Register<MainWindowViewModel, LogOutMessage>(this, (recipient, message) =>
            {
                CurrentPage = new LoginPageViewModel();
                IsPaneOpen = false;
                PaneLength = 5;

                // Remove HistoryPageViewModel from the list
                Items.Remove(_historyItem);
            });

            messenger.Register<MainWindowViewModel, GuestModeMessage>(this, (recipient, message) =>
            {
                CurrentPage = new ProductsPageViewModel();
                IsPaneOpen = false;

                LabelLog = "Login";
                BtnColor = new SolidColorBrush(Colors.White);
                LblColor = new SolidColorBrush(Colors.Black);
                PaneLength = 65;

                // Remove HistoryPageViewModel for guest mode
                Items.Remove(_historyItem);
            });
        }

        [ObservableProperty]
        private bool _isPaneOpen;

        [ObservableProperty]
        private ViewModelBase _currentPage = new LoginPageViewModel();

        [ObservableProperty]
        private ListItemTemplate? _selectedListItem;

        partial void OnSelectedListItemChanged(ListItemTemplate? value)
        {
            if (value is null) return;
            var instance = Activator.CreateInstance(value.ModelType);
            if (instance is null) return;
            CurrentPage = (ViewModelBase)instance;
        }

        public ObservableCollection<ListItemTemplate> Items { get; } = new ObservableCollection<ListItemTemplate>
        {
            new ListItemTemplate(typeof(ProductsPageViewModel), "buildingshopregular")
        };

        [ObservableProperty]
        private string _labelLog = "Login";

        [ObservableProperty]
        private Brush _btnColor = new SolidColorBrush(Colors.White);

        [ObservableProperty]
        private Brush _lblColor = new SolidColorBrush(Colors.Black);

        [ObservableProperty]
        private int _paneLength = 5;

        [RelayCommand]
        private void TriggerPane()
        {
            IsPaneOpen = !IsPaneOpen;
        }

        [RelayCommand]
        private void LogOut()
        {
            WeakReferenceMessenger.Default.Send(new LogOutMessage());
        }

        [RelayCommand]
        private void IsAdminTrue()
        {
            WeakReferenceMessenger.Default.Send(new AdminFalseMessage());
        }
    }

    public class ListItemTemplate
    {
        public ListItemTemplate(Type type, string iconKey)
        {
            ModelType = type;
            Label = type.Name.Replace("PageViewModel", "");

            Application.Current!.TryFindResource(iconKey, out var res);
            ListItemIcon = (StreamGeometry)res!;
        }
        public string Label { get; }

        public Type ModelType { get; }
        public StreamGeometry ListItemIcon { get; }
    }
}
