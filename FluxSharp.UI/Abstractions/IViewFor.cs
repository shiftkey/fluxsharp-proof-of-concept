using System;
using System.Windows;
using Splat;

namespace FluxSharp.UI
{
    public interface IFluxViewFor<T> where T : DataStore
    {
        T Store { get; }
    }

    public interface IFluxControl<T>
    {

    }

    public static class ViewExtensions
    {
        public static void OnChange<T>(this IFluxViewFor<T> view, Action<T> callback) where T : DataStore
        {
            // TODO: defer this registration until we have a store
            if (view.Store == null)
            {
                return;
            }

            view.Store.AppDispatcher.Register<ChangePayload>(
                payload => callback(view.Store));
        }

        public static void OnUpdated<T>(this IFluxControl<T> view, Action<T> callback) where T : class
        {
            var control = view as FrameworkElement;
            if (control == null)
            {
                // TODO: we should fail the app
                return;
            }

            control.DataContextChanged += (sender, args) =>
            {
                var newItem = args.NewValue as T;
                callback(newItem);
            };
        }

        public static void Dispatch<TView,TPayload>(this IFluxViewFor<TView> view, TPayload payload) where TView : DataStore
        {
            var appDispatcher = Locator.Current.GetService(typeof(Dispatcher)) as Dispatcher;

            if (appDispatcher == null)
            {
                // TODO: we should fail the app
                return;
            }

            appDispatcher.Dispatch(payload);
        }

        public static void Dispatch<TView, TPayload>(this IFluxControl<TView> view, TPayload payload)
        {
            var appDispatcher = Locator.Current.GetService(typeof(Dispatcher)) as Dispatcher;

            if (appDispatcher == null)
            {
                // TODO: we should fail the app
                return;
            }

            appDispatcher.Dispatch(payload);
        }

    }

}