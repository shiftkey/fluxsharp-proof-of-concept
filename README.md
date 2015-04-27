# Flux C# Proof of Concept

This is the result of me wrapping my head around Flux/React concepts -
by implementing a simple library in C# to emulate the behaviour.

It's super-rough, but as a first step I'm porting the
[Flux Todo App](https://github.com/facebook/flux/blob/2e6238c632dcaf276a303bc3239d7c273b94f9fd/docs/TodoList.md)
and experimenting with what shakes out as an API.

Tasks:

 - [x] can add items and mark them as done
 - [x] can check/uncheck all items in header
 - [ ] can edit task name
 - [x] display count of tasks to complete in footer
 - [x] clear completed tasks from list

After that, I'll dig into some more things that interest me:

 - [ ] testing components - what does this look like?
 - [ ] dealing with complex stores - composition?
 - [ ] hiding away the plumbing of Splat
 - [ ] cleanup the hacks scattered everywhere

## Components

**Dispatcher** - not to be confused with WPF's `Dispatcher` - a Flux Dispatcher
is used to allow components to register callbacks for specific events, and then
other components can dispatch events. Rather than use magic strings, I've settled
on using POCO classes for these events - it feels a bit CQRS-y, but that's fine
with me so far.

**Stores** - the store contains the logic and state for the application. It subscribes
to specific events and, after updating itself, will signal to the view that changes
are available for consumption.

```csharp
public class ToDoStore : Store
{
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

        // other registrations here
    }
}
```

**View** - in Flux the view is responsible for two things:

 - receiving new changes and updating the UI
 - handling user interactions and raising new events on the dispatcher

```csharp
public partial class HeaderView : IFluxViewFor<ToDoStore>
{
    public HeaderView()
    {
        InitializeComponent();

        // some ceremony here as part of setup

        this.OnChange(store =>
        {
            newToDo.Text = store.GetText();

            var createTextObs = Observable.Merge(
                newToDo.Events().LostFocus
                    .SelectUnit(),
                newToDo.Events().KeyDown
                    .Where(x => x.Key == Key.Enter)
                    .SelectUnit());

            disposable.Disposable = createTextObs
                .Subscribe(_ => this.Dispatch(new CreateItemAction(newToDo.Text))),

        });
    }

    // some other code here
}
```
The goal with views is to be as immutable as possible. I was skeptical at first, but
am finding it rather pleasant to make these views as dumb as possible.

I've not brought over the React `render` behaviour because I don't really care
about it at this stage - too much work for questionable benefits, and with XAML being
fairly declarative already I'm not really interested in reinventing that wheel.