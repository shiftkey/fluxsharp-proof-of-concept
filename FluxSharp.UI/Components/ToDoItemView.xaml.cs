using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using FluxSharp.Abstractions;
using FluxSharp.Actions;
using FluxSharp.Stores;

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
                IDisposable buttonClick;

                // TODO: need a cancel button here

                if (viewModel.IsEditable)
                {
                    editButton.Visibility = Visibility.Collapsed;
                    saveButton.Visibility = Visibility.Visible;

                    buttonClick = Observable.FromEventPattern<RoutedEventArgs>(saveButton, "Click")
                        .Subscribe(_ => this.Dispatch(new SaveItemAction(viewModel.Id, edit.Text)));

                    edit.Visibility = Visibility.Visible;
                    text.Visibility = Visibility.Collapsed;
                }
                else
                {
                    editButton.Visibility = Visibility.Visible;
                    saveButton.Visibility = Visibility.Collapsed;
                    
                    buttonClick = Observable.FromEventPattern<RoutedEventArgs>(editButton, "Click")
                        .Subscribe(_ => this.Dispatch(new EditItemAction(viewModel.Id)));

                    edit.Visibility = Visibility.Collapsed;
                    text.Visibility = Visibility.Visible;
                }

                text.Text = viewModel.Text;
                edit.Text = viewModel.Text;

                isChecked.IsChecked = viewModel.IsComplete;

                var checkedChanged = isChecked.IsChecked == false
                    ? Observable.FromEventPattern<RoutedEventArgs>(isChecked, "Checked")
                        .Subscribe(_ => this.Dispatch(new CheckedItemAction(viewModel.Id)))
                    : Observable.FromEventPattern<RoutedEventArgs>(isChecked, "Unchecked")
                        .Subscribe(_ => this.Dispatch(new UncheckedItemAction(viewModel.Id)));

                disposable.Disposable = new CompositeDisposable(buttonClick, checkedChanged);
            });
        }
    }
}
