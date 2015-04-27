using System;
using System.Windows;

namespace FluxSharp.Abstractions
{
    public static class FrameworkElementExtensions
    {
        public static void OnUpdated<T>(this FrameworkElement view, Action<T> callback) where T : class
        {
            view.DataContextChanged += (sender, args) =>
            {
                var newItem = args.NewValue as T;
                callback(newItem);
            };
        }
    }
}