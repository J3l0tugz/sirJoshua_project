//using System;
//using System.Diagnostics.CodeAnalysis;
//using Avalonia;
//using Avalonia.Controls.ApplicationLifetimes;
//using Avalonia.Markup.Xaml;
//using Mamilots_POS.Services;
//using Mamilots_POS.ViewModels;
//using Mamilots_POS.Views;
//using CommunityToolkit.Mvvm.DependencyInjection;
//using CommunityToolkit.Mvvm.Messaging;
//using System.Security.Authentication.ExtendedProtection;
//using System.Threading.Tasks;
//using Microsoft.Extensions.DependencyInjection;

//namespace Mamilots_POS
//{
//    public partial class App : Application
//    {
//        public override void Initialize()
//        {
//            AvaloniaXamlLoader.Load(this);
//        }

//        public override void OnFrameworkInitializationCompleted()
//        {
//            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
//            {
//                desktop.MainWindow = new MainWindow
//                {
//                    DataContext = new MainWindowViewModel()
//                };
//            }

//            base.OnFrameworkInitializationCompleted();
//        }
//    }
//}

using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Mamilots_POS.Services;
using Mamilots_POS.ViewModels;
using Mamilots_POS.Views;
using Mamilots_POS.Messages;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using System.Security.Authentication.ExtendedProtection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Mamilots_POS
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var locator = new ViewLocator();
            DataTemplates.Add(locator);

            var services = new ServiceCollection();
            ConfigureServices(services);
            services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);

            var provider = services.BuildServiceProvider();

            // Typed-clients
            // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-8.0#typed-clients
            services.AddHttpClient<ILoginService, LoginService>(httpClient => httpClient.BaseAddress = new Uri("https://dummyjson.com/"));

            Ioc.Default.ConfigureServices(provider);

            var vm = Ioc.Default.GetRequiredService<MainWindowViewModel>();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow(vm);
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();
            services.AddTransient<ProductsEditPageViewModel>();
            services.AddTransient<ProductsContentViewModel>();
            services.AddTransient<ProductsPageViewModel>();
            services.AddTransient<HistoryPageViewModel>();
            services.AddSingleton<FluentSplashScreenViewModel>();
            services.AddSingleton<LoginPageViewModel>();
            services.AddSingleton<SecretViewModel>();
            services.AddSingleton<INavigationService, NavigationService>();

            services.AddSingleton<MainWindow>();
            services.AddTransient<ProductsEditPageView>();
            services.AddTransient<ProductsPageView>();
            services.AddTransient<HistoryPageView>();
            services.AddTransient<FluentSplashScreenView>();
            services.AddTransient<LoginPageView>();
            services.AddTransient<SecretView>();
        }
    }
}
