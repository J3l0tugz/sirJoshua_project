using CommunityToolkit.Mvvm.Messaging.Messages;
using Mamilots_POS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamilots_POS.Messages
{
    public class LoginSuccessMessage(AuthenticationResult result) : ValueChangedMessage<AuthenticationResult>(result);
}
