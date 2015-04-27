using System.Windows;
using FluxSharp.UI.Stores;
using Splat;

namespace FluxSharp.UI
{
    public partial class App : Application
    {
        public App()
        {
            var dispatcher = new Dispatcher();
            Locator.CurrentMutable.RegisterConstant(dispatcher, typeof(Dispatcher));

            var todoStore = new ToDoStore();
            Locator.CurrentMutable.RegisterConstant(todoStore, typeof(ToDoStore));
        }
    }
}
