using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Mamilots_POS.Services;

namespace Mamilots_POS.ViewModels
{
    public partial class SecretViewModel : ViewModelBase
    {
        [ObservableProperty] private string _token;

        public SecretViewModel(AuthenticationResult authResult)
        {
            Trace.WriteLine("SecretViewModel Token: " + authResult.AccessToken);
            Token = authResult.AccessToken;
        }
    }
}
