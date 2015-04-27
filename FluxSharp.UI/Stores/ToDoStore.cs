using System;
using System.Collections;
using System.Collections.Generic;
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
