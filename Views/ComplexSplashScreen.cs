using Avalonia.Media;
using FluentAvalonia.UI.Windowing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mamilots_POS.Views
{
    public class ComplexSplashScreen : IApplicationSplashScreen
    {
        public ComplexSplashScreen()
        {
            SplashScreenContent = new FluentSplashScreenView();
        }

        public string AppName => "";
        public IImage? AppIcon => null;
        public object SplashScreenContent { get; }
        public int MinimumShowTime => 0;
        public async Task RunTasks(CancellationToken token)
        {
            await ((FluentSplashScreenView)SplashScreenContent).InitApp();
        }
    }
}
