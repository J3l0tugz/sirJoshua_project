using Mamilots_POS.ViewModels;
using System;

public interface INavigationService
{
    void Navigate<T>() where T : ViewModelBase;
}

public class NavigationService : INavigationService
{
    private readonly Func<Type, ViewModelBase> _viewModelFactory;
    private readonly IServiceProvider _serviceProvider;

    public NavigationService(Func<Type, ViewModelBase> viewModelFactory, IServiceProvider serviceProvider)
    {
        _viewModelFactory = viewModelFactory;
        _serviceProvider = serviceProvider;
    }

    public void Navigate<T>() where T : ViewModelBase
    {
        var viewModel = _viewModelFactory(typeof(T));
        // Implement your actual navigation logic here
        // This might involve changing the current view, using a frame, etc.
    }
}