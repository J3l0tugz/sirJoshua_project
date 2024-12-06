using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Mamilots_POS.ViewModels;
using FluentAvalonia.UI.Windowing;
using CommunityToolkit.Mvvm.Messaging;
using System;

namespace Mamilots_POS.Views
{
    public partial class MainWindow : AppWindow
    {
        public MainWindow(MainWindowViewModel vm)
        {
            InitializeComponent();
            DataContext = vm ?? throw new ArgumentNullException(nameof(vm));
            SplashScreen = new ComplexSplashScreen();

            TitleBar.ExtendsContentIntoTitleBar = true;
            TitleBar.TitleBarHitTestType = TitleBarHitTestType.Complex;

            var button = this.FindControl<Button>("BtnCollapse");
            button.PointerEntered += OnPointerEnter;
            button.PointerExited += OnPointerLeave;

            this.MinWidth = 799;
            this.MinHeight = 800;

            this.PropertyChanged += MainWindow_PropertyChanged;
        }

        //public MainWindow() : this(new MainWindowViewModel(WeakReferenceMessenger.Default)) { } WHERE DID THIS COME FROM
        public MainWindow() : this(new MainWindowViewModel()) { }
        private void OnPointerEnter(object sender, PointerEventArgs e)
        {
            this.Cursor = new Cursor(StandardCursorType.Hand);
        }

        private void OnPointerLeave(object sender, PointerEventArgs e)
        {
            this.Cursor = new Cursor(StandardCursorType.Arrow);
        }

        private void MainWindow_PropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name == "ClientSize" && DataContext is MainWindowViewModel viewModel)
            {
                var width = this.ClientSize.Width;
                var isPaneOpen = viewModel.IsPaneOpen;

                // Adjust pane visibility based on window width
                viewModel.IsPaneOpen = width >= 600 ? isPaneOpen : false;
            }
        }
    }
}
