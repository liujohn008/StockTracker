using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Unity;
using StockTracker;

namespace StockTracker_Unity
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    public partial class App : Application
    {
        IUnityContainer Container;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjets();
            Application.Current.MainWindow.Show();
        }

        private void ComposeObjets()
        {
            
            Application.Current.MainWindow = Container.Resolve<StockTracker.View.StockTrackerView>();
            Application.Current.MainWindow.Title = "DI with Unity - Stock Tracker";
        }

        private void ConfigureContainer()
        {
            Container = new UnityContainer();
        }
    }
}
