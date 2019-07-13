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
    public partial class StartUC : Grid
    {
        ViewModelDataBaseClass ViewModel { get; set; }
        public StartUC()
        {
            DataContextChanged += StartUC_DataContextChanged;
            InitializeComponent();
        }

        /// <summary>"Вытягивание" ViewModel из контекста данных</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartUC_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ViewModel = e.NewValue as ViewModelDataBaseClass;
        }
    }
}
