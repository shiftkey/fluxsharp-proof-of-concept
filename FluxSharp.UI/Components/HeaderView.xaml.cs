using System;
using FluxSharp.UI.Stores;

namespace FluxSharp.UI.Components
{
    public partial class HeaderView : IViewFor<ToDoStore>
    {
        public HeaderView()
        {
            InitializeComponent();

            this.OnChange(store => {
                // ViewModel = store.GetAll();
            });

            //this.WhenActivated(d =>
            //{
            //    d(newToDo.Events().KeyDown
            //        .Where(x => x.Key == Keys.Enter)
            //        .Subscribe(_ =>
            //        {
            //            this.AppDispatcher.Publish(new CreateAction(newToDo.Text));
            //        }));
            //});
        }

        public ToDoStore Store { get; private set; }
    }
}
