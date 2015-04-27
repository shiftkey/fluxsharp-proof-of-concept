using System;
using System.Collections.Generic;
using System.Linq;
using FluxSharp.Abstractions;
using FluxSharp.Actions;

namespace FluxSharp.Stores
{
    public class ToDoStore : Store
    {
        readonly Dictionary<string, ToDoItem> items
            = new Dictionary<string, ToDoItem>();

        readonly Random random = new Random();

        public ToDoStore()
        {
            AppDispatcher.Register<CreateItemAction>(
                create =>
            {
                var newText = create.Message.Trim();
                if (string.IsNullOrWhiteSpace(newText))
                {
                    return;
                }

                Create(newText);
                text = "";
                EmitChange();
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
                    // TODO: implement this
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

            AppDispatcher.Register<ClearCompletedTasksAction>(
                action =>
                {
                    foreach (var item in items.Where(x => x.Value.IsComplete).ToList())
                    {
                        items.Remove(item.Key);
                    }
                    EmitChange();
                });
        }

        public bool GetAllChecked()
        {
            return items.Any() && items.Values.All(a => a.IsComplete);
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

        void Create(string newText)
        {
            var now = DateTimeOffset.Now;
            var offset = Math.Floor(random.NextDouble() * 999999);

            var id = string.Format("{0}{1}", now, offset);
            var item = new ToDoItem
            {
                Text = newText,
                Id = id,
                IsComplete = false
            };

            items[id] = item;
        }
    }
}
