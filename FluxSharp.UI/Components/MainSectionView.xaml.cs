using System;
using System.ComponentModel;
using FluxSharp.UI.Stores;
using Splat;

namespace FluxSharp.UI.Components
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
