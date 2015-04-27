namespace FluxSharp.Abstractions
{
    public interface IFluxViewFor<out T> where T : Store
    {
        T Store { get; }
    }
}