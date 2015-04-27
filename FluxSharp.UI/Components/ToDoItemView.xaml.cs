using System;
using System.Reactive.Linq;
using System.Windows;
using FluxSharp.UI.Actions;
using FluxSharp.UI.Stores;
using ReactiveUI;
using Splat;

namespace FluxSharp.UI.Components
{
    public partial class ToDoItemView : IViewFor<ToDoItem>
    {
        public ToDoItemView()
        {
            InitializeComponent();

            DataContextChanged += (s, e) => ViewModel = e.NewValue as ToDoItem;

            AppDispatcher = Locator.Current.GetService(typeof(Dispatcher)) as Dispatcher;

            this.WhenActivated(d =>
            {
                d(this.OneWayBind(ViewModel, vm => vm.Text, v => v.text.Text));
                d(this.OneWayBind(ViewModel, vm => vm.IsComplete, v => v.isChecked.IsChecked));

                // TODO: we should only need to bind one callback here

                var checkedObs = Observable.FromEventPattern<RoutedEventArgs>(isChecked, "Checked");
                d(checkedObs.Subscribe(_ =>
                {
                    AppDispatcher.Dispatch(new CheckedItemAction(ViewModel.Id));
                }));

                var uncheckedObs = Observable.FromEventPattern<RoutedEventArgs>(isChecked, "Unchecked");
                d(uncheckedObs.Subscribe(_ =>
                {
                    AppDispatcher.Dispatch(new UncheckedItemAction(ViewModel.Id));
                }));

            });
        }

        public Dispatcher AppDispatcher { get; private set; }

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
