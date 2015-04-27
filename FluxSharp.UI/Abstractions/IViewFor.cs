using System;
using System.Windows;
using FluxSharp.Abstractions;
using FluxSharp.Stores;
using Splat;

namespace FluxSharp.UI
{
    public static class ViewExtensions
    {
        public static void OnChange<T>(this IFluxViewFor<T> view, Action<T> callback) where T : Store
        {
            var appDispatcher = Locator.Current.GetService(typeof(Dispatcher)) as Dispatcher;
            var store = Locator.Current.GetService(typeof(T)) as T;

            if (appDispatcher == null)
            {
                // TODO: we should fail the app
                return;
            }

            appDispatcher.Register<ChangePayload>(
                payload => callback(store));
        }

        public static void OnUpdated<T>(this IFluxControl view, Action<T> callback) where T : class
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

        public static void Dispatch<TView,TPayload>(this IFluxViewFor<TView> view, TPayload payload) where TView : Store
        {
            var appDispatcher = Locator.Current.GetService(typeof(Dispatcher)) as Dispatcher;

            if (appDispatcher == null)
            {
                // TODO: we should fail the app
                return;
            }

            appDispatcher.Dispatch(payload);
        }

        public static void EmitChange<TView>(this IFluxViewFor<TView> view) where TView : Store
        {
            view.Dispatch(new ChangePayload());
        }

        public static void Dispatch<TPayload>(this IFluxControl view, TPayload payload)
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