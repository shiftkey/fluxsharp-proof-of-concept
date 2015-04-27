using Splat;

namespace FluxSharp.Abstractions
{
    public interface IFluxControl
    {

    }

    public static class FluxControlExtensions
    {
        public static void Dispatch<TPayload>(this IFluxControl view, TPayload payload)
        {
            var appDispatcher = Locator.Current.GetService<Dispatcher>();
            if (appDispatcher == null)
            {
                // TODO: we should fail the app
                return;
            }

            appDispatcher.Dispatch(payload);
        }
    }
}