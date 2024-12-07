using Avalonia.Media.Imaging;
using Mamilots_POS.Helpers;
using Mamilots_POS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Mamilots_POS.ViewModels
{
    
    
        internal partial class HistoryPageViewModel : ViewModelBase, INotifyPropertyChanged
        {
            private ObservableCollection<transaction> _transactions;

            public ObservableCollection<transaction> transactions
            {
                get => _transactions;
                set
                {
                    if (_transactions != value)
                    {
                        _transactions = value;
                        OnProductPropertyChanged();
                    }
                }
            }

            public HistoryPageViewModel(AuthenticationResult authResult)
            {

            }

            // Constructor to set the transactionNames
            public HistoryPageViewModel()
            {
                transactions = new ObservableCollection<transaction>
            {
            new transaction { OrderNumber = "#01", TotalPrice = 120.00},
            new transaction { OrderNumber = "#02", TotalPrice = 5000.00}

                };
            }

            public event PropertyChangedEventHandler ProductPropertyChanged;

            protected void OnProductPropertyChanged([CallerMemberName] string propertyName = null)
            {
                ProductPropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }


        }

        public class transaction
        {

            public string? OrderNumber { get; set; }
            public Double? TotalPrice { get; set; }

    }

}
