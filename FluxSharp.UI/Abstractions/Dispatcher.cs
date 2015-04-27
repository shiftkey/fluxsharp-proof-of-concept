using System;
using System.Collections.Generic;

namespace FluxSharp.UI
{
    public class Dispatcher
    {
        Dictionary<Type, List<object>> callbacks
            = new Dictionary<Type, List<object>>();

        public void Register<T>(Action<T> callback)
        {
            var key = typeof(T);

            if (callbacks.ContainsKey(key))
            {
                var currentCallbacks = callbacks[key];
                currentCallbacks.Add(callback);
            }
            else
            {
                var newCallbacks = new List<object> { callback };
                callbacks.Add(key, newCallbacks);
            }
        }

        public void Dispatch<T>(T payload)
        {
            var key = typeof(T);

            if (callbacks.ContainsKey(key))
            {
                var currentCallbacks = callbacks[key];
                foreach (var c in currentCallbacks)
                {
                    Action<T> convertedCallback = c as Action<T>;
                    if (convertedCallback == null)
                    {
                        // TODO: whut?
                    }
                    else
                    {
                        convertedCallback(payload);
                    }
                }
            }
            else
            {
                // TODO: whut?
            }
        }
    }
}
