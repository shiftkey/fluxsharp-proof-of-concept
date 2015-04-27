namespace FluxSharp.Actions
{
    public class UpdateTextAction
    {
        public UpdateTextAction(string id, string message)
        {
            Id = id;
            Message = message;
        }

        public string Id { get; private set; }
        public string Message { get; private set; }
    }
}