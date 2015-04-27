using System.Windows;
using Splat;

namespace FluxSharp.UI
{
    public partial class App : Application
    {
        public App()
        {
            var dispatcher = new Dispatcher();

            Locator.CurrentMutable.RegisterConstant(dispatcher, typeof(Dispatcher));
        }
    }
}
