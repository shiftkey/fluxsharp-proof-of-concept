using System;
using System.Windows;

namespace FluxSharp.UI
{
    public interface IFluxViewFor<T> where T : DataStore
    {
        T Store { get; }
        Dispatcher AppDispatcher { get; }
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
    }

}