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
    /// Логика взаимодействия для WinGame.xaml
    /// </summary>
    public partial class WinGame : Window
    {
        public WinGame()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) => Environment.Exit(0);
    }
}
