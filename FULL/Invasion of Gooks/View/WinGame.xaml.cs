using System;
using System.Windows;

namespace Invasion_of_Gooks.View
{
    /// <summary>
    /// Логика взаимодействия для WinGame.xaml
    /// </summary>
    public partial class WinGame : Window
    {
        public WinGame()
        {
            InitializeComponent();
        }
        //public ViewModelClass viewModel;
        //public Sky warSky;
        //BattleWind battleWind;

        //public WinGame(BattleWind battleWind) : this()
        //{

        //}

        private void Button_Click(object sender, RoutedEventArgs e) => Environment.Exit(0);

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //warSky.timer.Stop();       
            //BattleWind battleWind = new BattleWind();
            //MessageBox.Show("слежу");


            //battleWind.Close();
            this.Close();

            //MainWindow mainWindow = new MainWindow();
            //mainWindow.Show();
        }
    }
}
