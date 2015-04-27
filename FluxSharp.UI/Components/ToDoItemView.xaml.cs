using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using FluxSharp.Abstractions;
using FluxSharp.Actions;
using FluxSharp.Stores;
using FluxSharp.UI;

namespace FluxSharp.Components
{
    public partial class ToDoItemView : IFluxControl
    {
        public ToDoItemView()
        {
            InitializeComponent();

            var disposable = new SerialDisposable();

            this.OnUpdated<ToDoItem>(viewModel =>
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
