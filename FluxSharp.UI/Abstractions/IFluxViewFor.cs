using System;
using Splat;

namespace FluxSharp.Abstractions
{
    public interface IFluxViewFor<out T> where T : Store
    {

    }

    public static class FluxViewExtensions
    {
        public static void OnChange<T>(this IFluxViewFor<T> view, Action<T> callback) where T : Store
        {
            var appDispatcher = Locator.Current.GetService<Dispatcher>();

            if (appDispatcher == null)
            {
                // TODO: we should fail the app
                return;
            }

            var lazyStore = new Lazy<T>(() => Locator.Current.GetService<T>());
            appDispatcher.Register<ChangePayload>(payload => callback(lazyStore.Value));

            // lol hiding the hax away
            view.EmitChange();
        }

        public static void Dispatch<TView, TPayload>(this IFluxViewFor<TView> view, TPayload payload) where TView : Store
        {
            var appDispatcher = Locator.Current.GetService<Dispatcher>();
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
    }
}