using System.Windows;
using FluxSharp.UI.Components;
using FluxSharp.UI.Stores;

namespace FluxSharp.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var app = new ToDoView()
            {
                Store = new ToDoStore()
            };
            Content = app;
        }
    }
}
