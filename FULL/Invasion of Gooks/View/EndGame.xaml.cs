using Invasion_of_Gooks.ViewModel;
using Invasion_of_Gooks.Model;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Invasion_of_Gooks.View
{
    /// <summary>
    /// Логика взаимодействия для EndGame.xaml
    /// </summary>
    public partial class EndGame : Window
    {
        //public ViewModelClass viewModel;

        //public GamerClass Gamer;
        //public Sky warSky;
        BattleWind battleWind;

        public EndGame()
        {
            InitializeComponent();
            //viewModel = (ViewModelClass)DataContext;
        }

        public EndGame(BattleWind battleWind):this()
        {
            this.battleWind = battleWind;
        }

        private void Exit_Click(object sender, RoutedEventArgs e) => Environment.Exit(0);

        private void Exit_To_Main_Window_Click(object sender, RoutedEventArgs e)
        {
            //warSky.timer.Stop();
           // battleWind.Close();
            this.Close();
           // MainWindow mainWindow = new MainWindow();
           // mainWindow.Show();
        }
    }
}
