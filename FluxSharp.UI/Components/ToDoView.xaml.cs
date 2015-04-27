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

            this.OnChange(store =>
            {
                // TODO: anything important here?
            });
        }

        public ToDoStore Store { get; set; }
    }
}
