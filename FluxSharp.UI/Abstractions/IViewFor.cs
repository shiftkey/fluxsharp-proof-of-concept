using System;

namespace FluxSharp.UI
{
    public interface IViewFor<T> where T : DataStore
    {
        T Store { get; }
    }

    public static class ViewExtension
    {
        public static void OnChange<T>(this IViewFor<T> view, Action<T> callback) where T : DataStore
        {
            view.Store.AppDispatcher.Register<ChangePayload>(
                payload => callback(view.Store));
        }
    }

}