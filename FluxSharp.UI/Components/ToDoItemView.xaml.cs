using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using FluxSharp.UI.Actions;
using FluxSharp.UI.Stores;
using Splat;

namespace FluxSharp.UI.Components
{
    public partial class ToDoItemView : IFluxControl<ToDoItem>
    {
        public ToDoItemView()
        {
            InitializeComponent();

            AppDispatcher = Locator.Current.GetService(typeof(Dispatcher)) as Dispatcher;

            SerialDisposable disposable = new SerialDisposable();

            // TODO: a better abstraction here?

            this.OnUpdated(viewModel =>
            {
                text.Text = viewModel.Text;
                isChecked.IsChecked = viewModel.IsComplete;

                disposable.Disposable = isChecked.IsChecked == false
                    ? Observable.FromEventPattern<RoutedEventArgs>(isChecked, "Checked")
                        .Subscribe(_ => AppDispatcher.Dispatch(new CheckedItemAction(viewModel.Id)))
                    : Observable.FromEventPattern<RoutedEventArgs>(isChecked, "Unchecked")
                        .Subscribe(_ => AppDispatcher.Dispatch(new UncheckedItemAction(viewModel.Id)));
            });
        }

        public Dispatcher AppDispatcher { get; private set; }
    }
}
