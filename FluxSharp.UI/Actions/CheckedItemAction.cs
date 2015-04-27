namespace FluxSharp.UI.Actions
{
    public class CheckedItemAction
    {
        public CheckedItemAction(string id)
        {
            Id = id;
        }

        public string Id { get; private set; }
    }

    public class UncheckedItemAction
    {
        public UncheckedItemAction(string id)
        {
            Id = id;
        }

        public string Id { get; private set; }
    }



    public class CreateItemAction
    {
        public CreateItemAction(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }

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

    public class ToggleAllCompletedAction
    {

    }
}
