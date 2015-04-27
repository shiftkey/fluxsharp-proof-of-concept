using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using FluxSharp.UI.Actions;
using FluxSharp.UI.Stores;

namespace FluxSharp.UI.Components
{
    public partial class ToDoItemView : IFluxControl<ToDoItem>
    {
        public ToDoItemView()
        {
            InitializeComponent();
            
            SerialDisposable disposable = new SerialDisposable();

            this.OnUpdated(viewModel =>
            {
                text.Text = viewModel.Text;
                isChecked.IsChecked = viewModel.IsComplete;

                disposable.Disposable = isChecked.IsChecked == false
                    ? Observable.FromEventPattern<RoutedEventArgs>(isChecked, "Checked")
                        .Subscribe(_ => this.Dispatch(new CheckedItemAction(viewModel.Id)))
                    : Observable.FromEventPattern<RoutedEventArgs>(isChecked, "Unchecked")
                        .Subscribe(_ => this.Dispatch(new UncheckedItemAction(viewModel.Id)));
            });
        }
    }
}
