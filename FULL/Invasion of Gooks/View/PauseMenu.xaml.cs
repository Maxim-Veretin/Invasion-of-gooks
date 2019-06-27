using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Invasion_of_Gooks.View
{
    public partial class PauseMenu : Window
    {
        public PauseMenu()
        {
            InitializeComponent();
        }
        //BattleWind battleWind; 
        
        //public PauseMenu(BattleWind battleWind) : this()
        //{
        //    this.battleWind = battleWind; 
        //}


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        //private void Exit_To_Main_Window_Click(object sender, RoutedEventArgs e)
        //{
        //    battleWind.Close();
        //    MainWindow mainWindow = new MainWindow();
        //    mainWindow.Show();
        //    this.Close();
        //}
    }
}