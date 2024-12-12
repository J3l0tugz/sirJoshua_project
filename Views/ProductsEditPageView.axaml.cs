using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.DependencyInjection;
using Mamilots_POS.ViewModels;

namespace Mamilots_POS.Views
{
    public partial class ProductsEditPageView : UserControl
    {
        public ProductsEditPageView()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<ProductsEditPageViewModel>();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
