namespace FluxSharp.Actions
{
    public class CreateItemAction
    {
        public CreateItemAction(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }
}