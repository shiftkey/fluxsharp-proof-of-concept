using System.ComponentModel;
using FluxSharp.Abstractions;
using FluxSharp.Stores;
using FluxSharp.UI;

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

            this.OnChange(store =>
            {
                todos.ItemsSource = store.GetAll();
            });

            // lol setup hax
            this.EmitChange();
        }
    }
}
