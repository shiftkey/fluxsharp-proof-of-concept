namespace FluxSharp.UI.Stores
{
    public class ToDoStore : DataStore
    {
        public ToDoStore()
        {
            AppDispatcher.Register<CreateAction>(
                create =>
            {
                var text = create.Message.Trim();
                if (!string.IsNullOrWhiteSpace(text))
                {
                    Create(text);
                    EmitChange();
                }
            });

            AppDispatcher.Register<UpdateTextAction>(
                update =>
                {

                });

            AppDispatcher.Register<ToggleAllCompletedAction>(
                allCompleted =>
                {

                });
        }




        private void Create(string text)
        {
            // TODO: add a new item to the collection
        }
    }



    public class CreateAction
    {
        public CreateAction(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }

    public class UpdateTextAction
    {
        public UpdateTextAction(int id, string message)
        {
            Id = id;
            Message = message;
        }

        public int Id { get; private set; }
        public string Message { get; private set; }
    }

    public class ToggleAllCompletedAction
    {

    }
}
