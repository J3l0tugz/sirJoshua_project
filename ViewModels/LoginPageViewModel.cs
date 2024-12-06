using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Mamilots_POS.Messages;
using Mamilots_POS.Services;
using Mamilots_POS.Utilities;

namespace Mamilots_POS.ViewModels
{
    public partial class LoginPageViewModel : ViewModelBase
    {
        [ObservableProperty] private string _errorMessage = "";
        [ObservableProperty] private string _username = "";
        [ObservableProperty] private string _password = "";
        [ObservableProperty] private IReadOnlyList<DummyUser> _availableUsers = [];
        [ObservableProperty] private DummyUser? _selectedUser;

        partial void OnSelectedUserChanged(DummyUser? value)
        {
            if (value is null) return;

            Username = value.Username;
            Password = value.Password;
        }

        private readonly ILoginService _loginService;
        private readonly IMessenger _messenger;

        public LoginPageViewModel() : this(new LoginService(new HttpClient { BaseAddress = new Uri("https://dummyjson.com/") }), MessengerInstance.Instance) { }
       
            public LoginPageViewModel(ILoginService loginService, IMessenger messenger)
        {
            _loginService = loginService;
            _messenger = messenger;
            _ = GetUsers();
        }

        //public LoginPageViewModel() : this(new LoginService(new HttpClient { BaseAddress = new Uri("https://dummyjson.com/")}), new WeakReferenceMessenger()) { }

        [RelayCommand]
        private async Task Login()
        {
            var authResult = await _loginService.Authenticate(Username, Password);

            if (authResult==null)
            {
                ErrorMessage = "Invalid username or password";
                Username = ""; // Clear the username field
                Password = ""; // Clear the password field
                return;
            }
            ErrorMessage = "";
            Trace.WriteLine("IT RUNS");
            Trace.WriteLine($"Sending LoginSuccessMessage with value: {authResult}");
            Trace.WriteLine("Authentication Result Token: " + authResult.AccessToken);
            _messenger.Send(new LoginSuccessMessage(authResult)); //Does this work?? 
        }

        private async Task GetUsers()
        {
            AvailableUsers = await _loginService.Users();
        }
    }
}
