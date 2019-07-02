using InvasionViewModel;
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
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        ViewModelDataBaseClass ViewModel { get; }
        public StartPage(ViewModelDataBaseClass viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = ViewModel;
        }

        private void Page_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void Page_LostFocus(object sender, RoutedEventArgs e)
        {

        }


    }
}
