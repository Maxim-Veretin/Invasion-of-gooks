using System;
using System.Windows;

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
