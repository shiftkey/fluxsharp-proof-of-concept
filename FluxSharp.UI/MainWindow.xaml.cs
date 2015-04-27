using System.Windows;
using FluxSharp.UI.Components;
using FluxSharp.UI.Stores;
using Splat;

namespace FluxSharp.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Content = new ToDoView();
        }
    }
}
