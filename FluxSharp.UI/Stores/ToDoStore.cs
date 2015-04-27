using System;
using System.Collections.Generic;
using System.Linq;
using FluxSharp.UI.Actions;

namespace FluxSharp.UI.Stores
{
    public class ToDoStore : DataStore
    {
        Dictionary<string, ToDoItem> items
            = new Dictionary<string, ToDoItem>();

        Random random = new Random();

        public ToDoStore() : base()
        {
            AppDispatcher.Register<CreateItemAction>(
                create =>
            {
                var text = create.Message.Trim();
                if (!string.IsNullOrWhiteSpace(text))
                {
                    Create(text);
                    this.text = "";
                    EmitChange();
                }
            });

            AppDispatcher.Register<CheckedItemAction>(
                action =>
                {
                    if (items.ContainsKey(action.Id))
                    {
                        var found = items[action.Id];
                        if (found.IsComplete)
                        {
                            System.Diagnostics.Debug.Fail("Item is not in a valid state for this action");
                        }
                        found.IsComplete = true;
                        EmitChange();
                    }
                });

            AppDispatcher.Register<UncheckedItemAction>(
                action =>
                {
                    if (items.ContainsKey(action.Id))
                    {
                        var found = items[action.Id];
                        if (!found.IsComplete)
                        {
                            System.Diagnostics.Debug.Fail("Item is not in a valid state for this action");
                        }
                        found.IsComplete = false;
                        EmitChange();
                    }
                });

            AppDispatcher.Register<UpdateTextAction>(
                update =>
                {

                });

            AppDispatcher.Register<ToggleAllCompletedAction>(
                action =>
                {
                    foreach(var item in items)
                    {
                        item.Value.IsComplete = action.IsCompleted;
                    }
                    EmitChange();
                });
        }

        public bool GetAllChecked()
        {
            return items.Values.All(a => a.IsComplete);
        }

        public IEnumerable<ToDoItem> GetAll()
        {
            return items.Values.ToList();
        }

        string text = "";
        public string GetText()
        {
            return text;
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

}
