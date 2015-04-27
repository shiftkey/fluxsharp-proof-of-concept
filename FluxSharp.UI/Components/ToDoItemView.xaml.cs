using System;
using System.Windows;
using FluxSharp.UI.Stores;
using ReactiveUI;

namespace FluxSharp.UI.Components
{
    public partial class ToDoItemView : IViewFor<ToDoItem>
    {
        public ToDoItemView()
        {
            InitializeComponent();

            DataContextChanged += (s, e) => ViewModel = e.NewValue as ToDoItem;

            this.WhenActivated(d =>
            {
                d(this.OneWayBind(ViewModel, vm => vm.Text, v => v.text.Text));
            });
        }

        public ToDoItem ViewModel
        {
            get { return (ToDoItem)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(ToDoItem), typeof(ToDoItemView), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = value as ToDoItem; }
        }
    }
}
