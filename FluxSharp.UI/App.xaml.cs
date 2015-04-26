using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FluxSharp.UI
{
    public partial class App : Application
    {
        public App()
        {
            AppDispatcher = new Dispatcher();



        }

        public static Dispatcher AppDispatcher { get; private set; }
    }
}
