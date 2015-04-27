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
 - [ ] display count of tasks to complete in footer
 - [ ] clear completed tasks from list

After that, I'll dig into some more things that interest me:

 - [ ] testing components - what does this look like?
 - [ ] dealing with complex stores - composition?
 - [ ] hiding away the plumbing of Splat
 - [ ] cleanup the hacks scattered everywhere
