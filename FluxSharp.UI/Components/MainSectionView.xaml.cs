using System.ComponentModel;
using FluxSharp.Abstractions;
using FluxSharp.Stores;
using FluxSharp.UI;
using Splat;

namespace FluxSharp.Components
{
    public partial class MainSectionView : IFluxViewFor<ToDoStore>
    {
        public MainSectionView()
        {
            InitializeComponent();

            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            Store = Locator.Current.GetService(typeof(ToDoStore)) as ToDoStore;
            
            this.OnChange(store =>
            {
                todos.ItemsSource = store.GetAll();
            });

            // lol setup hax
            Store.EmitChange();
        }

        public ToDoStore Store { get; set; }
    }
}
