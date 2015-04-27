using System.ComponentModel;
using FluxSharp.Abstractions;
using FluxSharp.Stores;
using FluxSharp.UI;
using Splat;

namespace FluxSharp.Components
{
    public partial class ToDoView : IFluxViewFor<ToDoStore>
    {
        public ToDoView()
        {
            InitializeComponent();

            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            Store = Locator.Current.GetService(typeof(ToDoStore)) as ToDoStore;
            AppDispatcher = Locator.Current.GetService(typeof(Dispatcher)) as Dispatcher;

            header.Store = Store;
            main.Store = Store;

            this.OnChange(store =>
            {
                // TODO: ???
            });
        }

        public Dispatcher AppDispatcher { get; private set; }

        public ToDoStore Store { get; set; }
    }
}
