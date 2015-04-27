namespace FluxSharp.Actions
{
    public class UncheckedItemAction
    {
        public UncheckedItemAction(string id)
        {
            Id = id;
        }

        public string Id { get; private set; }
    }
}