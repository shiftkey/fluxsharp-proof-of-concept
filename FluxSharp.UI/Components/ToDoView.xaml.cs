using FluxSharp.Abstractions;
using FluxSharp.Stores;

namespace FluxSharp.Components
{
    public partial class ToDoView : IFluxViewFor<ToDoStore>
    {
        public ToDoView()
        {
            InitializeComponent();
        }
    }
}
