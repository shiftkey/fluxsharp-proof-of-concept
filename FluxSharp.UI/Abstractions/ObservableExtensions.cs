using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

namespace FluxSharp.UI
{
    public static class ObservableExtensions
    {
        public static IObservable<Unit> SelectUnit<T>(this IObservable<T> observable)
        {
            return observable.Select(_ => Unit.Default);
        }
    }
}
