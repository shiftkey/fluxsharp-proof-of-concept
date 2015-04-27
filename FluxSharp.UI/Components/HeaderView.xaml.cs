using System;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Input;
using FluxSharp.UI.Actions;
using FluxSharp.UI.Stores;
using FluxSharp.UI.ViewModels;
using ReactiveUI;
using Splat;

namespace FluxSharp.UI.Components
{
    public partial class HeaderView : IViewFor<HeaderViewModel>, IFluxViewFor<ToDoStore>
    {
        public HeaderView()
        {
            InitializeComponent();

            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            Store = Locator.Current.GetService(typeof(ToDoStore)) as ToDoStore;
            AppDispatcher = Locator.Current.GetService(typeof(Dispatcher)) as Dispatcher;

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
                        .Subscribe(_ => AppDispatcher.Dispatch(new ToggleAllCompletedAction(false)))
                    : Observable.FromEventPattern<RoutedEventArgs>(allChecked, "Checked")
                        .Subscribe(_ => AppDispatcher.Dispatch(new ToggleAllCompletedAction(true)));

                disposable.Disposable = new CompositeDisposable(
                    createTextObs
                        .Subscribe(_ => AppDispatcher.Dispatch(new CreateItemAction(newToDo.Text))),
                    allCheckedDisp
                    );

            });

            // lol setup hax
            Store.EmitChange();
        }

        public ToDoStore Store { get; set; }

        public Dispatcher AppDispatcher { get; set; }


        // TODO: :fire: this dependency
        public HeaderViewModel ViewModel
        {
            get { return (HeaderViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(HeaderViewModel), typeof(HeaderView), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = value as HeaderViewModel; }
        }
    }
}
