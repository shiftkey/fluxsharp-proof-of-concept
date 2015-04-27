namespace FluxSharp.Actions
{
    public class ToggleAllCompletedAction
    {
        public ToggleAllCompletedAction(bool isCompleted)
        {
            IsCompleted = isCompleted;
        }

        public bool IsCompleted { get; private set; }
    }
}