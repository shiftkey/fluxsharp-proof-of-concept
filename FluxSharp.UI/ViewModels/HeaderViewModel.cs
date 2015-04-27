using ReactiveUI;

namespace FluxSharp.UI.ViewModels
{
    public class HeaderViewModel : ReactiveObject
    {
        public HeaderViewModel(string text)
        {
            Text = text;
        }

        string text;
        public string Text
        {
            get { return text; }
            set { this.RaiseAndSetIfChanged(ref text, value); }
        }
    }
}
