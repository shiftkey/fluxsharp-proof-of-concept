using System.Windows;
using FluxSharp.Abstractions;
using FluxSharp.Stores;
using Splat;

namespace FluxSharp
{
    public partial class App
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
