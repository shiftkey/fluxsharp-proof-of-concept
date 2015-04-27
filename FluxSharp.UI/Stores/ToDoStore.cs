using System;
using System.Collections;
using System.Collections.Generic;

namespace FluxSharp.UI.Stores
{
    public class ToDoStore : DataStore
    {
        Dictionary<string, ToDoItem> items
            = new Dictionary<string, ToDoItem>();

        Random random = new Random();

        public ToDoStore()
        {
            AppDispatcher.Register<CreateItemAction>(
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

        internal IEnumerable<ToDoItem> GetAll()
        {
            return items.Values;
        }
        
        public string GetText()
        {
            throw new NotImplementedException();
        }

        void Create(string text)
        {
            var now = DateTimeOffset.Now;
            var offset = Math.Floor(random.NextDouble() * 999999);

            var id = string.Format("{0}{1}", now, offset);
            var item = new ToDoItem
            {
                Text = text,
                Id = id,
                IsComplete = false
            };

            items[id] = item;
        }
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
