using System;
using System.Threading.Tasks;

namespace FluxSharp.UI
{
    public class Dispatcher
    {
        public void Register<T>(Action<T> callback)
        {

        }

        public void Register<T>(Func<T, Task> callback)
        {

        }

        public void Dispatch<T>(T payload)
        {

        }
    }
}
