using System;
using Splat;

namespace FluxSharp.Abstractions
{
    public abstract class Store
    {
        protected Store()
        {
            AppDispatcher = Locator.Current.GetService(typeof(Dispatcher)) as Dispatcher;
        }

        public Dispatcher AppDispatcher { get; private set; }

        public void EmitChange()
        {
            AppDispatcher.Dispatch(new ChangePayload());
        }
    }
}