namespace FluxSharp.Abstractions
{
    public interface IFluxViewFor<out T> where T : DataStore
    {
        T Store { get; }
    }
}