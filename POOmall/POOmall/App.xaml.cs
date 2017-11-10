using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using POOmall;

namespace POOmall
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        
        public App()
        {
            Startup += App_Startup;


        }

        public void App_Startup(object sender, StartupEventArgs e)
        {
            POOmall.MainWindow.Instance.Show();
        }
    }



}
