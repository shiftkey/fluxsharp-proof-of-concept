using System;

namespace FluxSharp.UI
{
    public interface IFluxViewFor<T> where T : DataStore
    {
        T Store { get; }
        Dispatcher AppDispatcher { get; }
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
    }

}