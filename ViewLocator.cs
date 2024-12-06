//using System;
//using Avalonia.Controls;
//using Avalonia.Controls.Templates;
//using Mamilots_POS.ViewModels;

//namespace Mamilots_POS
//{
//    public class ViewLocator : IDataTemplate
//    {
//        public Control Build(object? data)
//        {
//            if (data is null)
//            {
//                return new TextBlock { Text = "No ViewModel provided" };
//            }

//            var name = data.GetType().FullName!.Replace("ViewModel", "View");
//            var type = Type.GetType(name);

//            if (type != null)
//            {
//                return (Control)Activator.CreateInstance(type)!;
//            }

//            return new TextBlock { Text = "Not Found: " + name };
//        }

//        public bool Match(object? data)
//        {
//            return data is ViewModelBase;
//        }
//    }
//}

using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Mamilots_POS.ViewModels;
using Mamilots_POS.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace Mamilots_POS
{
    public class ViewLocator : IDataTemplate
    {
        private readonly Dictionary<Type, Func<Control?>> _locator = new();

        public ViewLocator() {
            RegisterViewFactory<MainWindowViewModel, MainWindow>();
            RegisterViewFactory<HomePageViewModel, HomePageView>();
            RegisterViewFactory<ProductsPageViewModel, ProductsPageView>();
            RegisterViewFactory<HistoryPageViewModel, HistoryPageView>();
            RegisterViewFactory<FluentSplashScreenViewModel, FluentSplashScreenView>();
            RegisterViewFactory<LoginPageViewModel, LoginPageView>();
            RegisterViewFactory<SecretViewModel, SecretView>();
        }

        public Control Build(object? data)
        {
            if (data is null)
            {
                return new TextBlock { Text = "No VM provided" };
            }

            _locator.TryGetValue(data.GetType(), out var factory);

            return factory?.Invoke() ?? new TextBlock { Text = $"VM Not Registered: {data.GetType()}" };
        }

        public bool Match(object? data)
        {
            return data is ObservableObject;
        }

        private void RegisterViewFactory<TViewModel, TView>()
            where TViewModel : class
            where TView : Control
            => _locator.Add(
                typeof(TViewModel),
                Design.IsDesignMode
                    ? Activator.CreateInstance<TView>
                    : Ioc.Default.GetService<TView>);
    }
}


