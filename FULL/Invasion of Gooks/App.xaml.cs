using Invasion_of_Gooks.Properties;
using InvasionViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Media;
using System.Resources;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Invasion_of_Gooks
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        readonly MainWindow mainWindow;
        readonly ViewModelBattleClass viewModelBattle;
        readonly ViewModelDataBaseClass viewModelDataBase;
        private bool isExit  = false;

        public App()
        {
            viewModelDataBase = new ViewModelDataBaseClass
                (
                    StartMetod,
                    (p) => true,
                    ExitMetod,
                    (p) => true
                );
            viewModelBattle = new ViewModelBattleClass();
            mainWindow = new MainWindow();
            mainWindow.Closing += MainWindow_Closing;
        }

        private void StartMetod(object parameter)
        {
            mainWindow.DataContext = viewModelBattle;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel=!isExit;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            mainWindow.DataContext = viewModelDataBase;
            mainWindow.Show();
        }

        private void ExitMetod(object parameter)
        {
            isExit = true;
            mainWindow.Close();
        }
    }
}
