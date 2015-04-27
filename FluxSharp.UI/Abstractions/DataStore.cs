using System;
using Splat;

namespace FluxSharp.UI
{
    public abstract class DataStore
    {
        public DataStore()
        {
            AppDispatcher = Locator.Current.GetService(typeof(Dispatcher)) as Dispatcher;
        }

        public Dispatcher AppDispatcher { get; private set; }

        public void EmitChange()
        {
            AppDispatcher.Dispatch(new ChangePayload());
        }
    }

    public class ChangePayload
    {

    }
}