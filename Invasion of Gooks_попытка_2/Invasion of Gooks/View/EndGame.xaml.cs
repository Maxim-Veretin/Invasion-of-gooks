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

namespace Invasion_of_Gooks.View
{
    /// <summary>
    /// Логика взаимодействия для EndGame.xaml
    /// </summary>
    public partial class EndGame : Window
    {
        public ViewModelClass viewModel;

        public GamerClass Gamer;
        public Sky Sky;

        public EndGame()
        {
            InitializeComponent();
            viewModel = (ViewModelClass)DataContext;
        }

        private void Fight_Click(object sender, RoutedEventArgs e)
        {
            //Gamer.Health = 10;
            Sky.timer.Start();
            viewModel.gamePlayer.Play();
            viewModel.gamePlayerPropellers.Play();
        }

        private void Exit_Click(object sender, RoutedEventArgs e) => Environment.Exit(0);
    }
}
