using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using FluxSharp.Abstractions;
using FluxSharp.Actions;
using FluxSharp.Stores;

namespace FluxSharp.Components
{
    public partial class FooterView : IFluxViewFor<ToDoStore>
    {
        public FooterView()
        {
            InitializeComponent();

            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            this.OnChange(store =>
            {
                var items = store.GetAll();
                var completed = items.Count(x => x.IsComplete);
                var incomplete = items.Count() - completed;

                counter.Text = string.Format("{0} item{1} left",
                    incomplete, incomplete > 1 ? "s" : "");

                clear.Visibility = completed > 0 ? Visibility.Visible : Visibility.Collapsed;

                if (completed > 0)
                {
                    clear.Events().Click.Subscribe(_ =>
                    {
                        this.Dispatch(new ClearCompletedTasksAction());
                    });
                }
            });
        }
    }
}
