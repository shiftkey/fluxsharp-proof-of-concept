using FluxSharp.UI.Stores;

namespace FluxSharp.UI.Components
{
    public partial class ToDoView : IViewFor<ToDoStore>
    {
        public ToDoView()
        {
            InitializeComponent();

            this.OnChange(store => {
                // ViewModel = store.GetAll();
            });

            // TODO: bindings
        }

        public ToDoStore Store { get; set; }
    }
}
