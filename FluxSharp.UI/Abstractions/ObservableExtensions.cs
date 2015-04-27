using System;
using System.Reactive;
using System.Reactive.Linq;

namespace FluxSharp.Abstractions
{
    public static class ObservableExtensions
    {
        public static IObservable<Unit> SelectUnit<T>(this IObservable<T> observable)
        {
            return observable.Select(_ => Unit.Default);
        }
    }
}
