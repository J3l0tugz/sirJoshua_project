using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamilots_POS.Utilities
{
    public static class MessengerInstance { public static IMessenger Instance { get; } = WeakReferenceMessenger.Default; }
}
