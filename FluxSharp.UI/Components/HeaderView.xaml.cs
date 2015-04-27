using System;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Input;
using FluxSharp.Abstractions;
using FluxSharp.Actions;
using FluxSharp.Stores;
using FluxSharp.UI;
using Splat;

namespace FluxSharp.Components
{
    public partial class HeaderView : IFluxViewFor<ToDoStore>
    {
        public HeaderView()
        {
            InitializeComponent();

            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            Store = Locator.Current.GetService(typeof(ToDoStore)) as ToDoStore;

            // TODO: should we make the API return an optional disposable?
            var disposable = new SerialDisposable();

            this.OnChange(store =>
            {
                newToDo.Text = store.GetText();

                var createTextObs = Observable.Merge(
                    newToDo.Events().LostFocus
                        .SelectUnit(),
                    newToDo.Events().KeyDown
                        .Where(x => x.Key == Key.Enter)
                        .SelectUnit());

                var allTasksChecked = store.GetAllChecked();
                allChecked.IsChecked = allTasksChecked;

                var allCheckedDisp = allTasksChecked
                    ? Observable.FromEventPattern<RoutedEventArgs>(allChecked, "Unchecked")
                        .Subscribe(_ => this.Dispatch(new ToggleAllCompletedAction(false)))
                    : Observable.FromEventPattern<RoutedEventArgs>(allChecked, "Checked")
                        .Subscribe(_ => this.Dispatch(new ToggleAllCompletedAction(true)));

                disposable.Disposable = new CompositeDisposable(
                    createTextObs
                        .Subscribe(_ => this.Dispatch(new CreateItemAction(newToDo.Text))),
                    allCheckedDisp);

            });

            // lol setup hax
            this.EmitChange();
        }

        public ToDoStore Store { get; private set; }
    }
}
