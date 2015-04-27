using System;
using FluxSharp.UI;
using Splat;

namespace FluxSharp.Abstractions
{
    public abstract class DataStore
    {
        protected DataStore()
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