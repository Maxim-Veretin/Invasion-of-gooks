using Invasion_of_Gooks.ViewModel;
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
    /// Логика взаимодействия для BattleWind.xaml
    /// </summary>
    public partial class BattleWind : Window
    {
        ViewModelClass viewModel;

        public BattleWind()
        {
            InitializeComponent();

            viewModel = (ViewModelClass)DataContext;
        }
        
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                //если нажата стрелочка вверх, то коордианта вертолётика смещается вверх
                case Key.Up:
                    viewModel.KeyUser(KeyControl.Up);
                    break;

                //если нажата стрелочка вправо, то коордианта вертолётика смещается вправо
                case Key.Right:
                    viewModel.KeyUser(KeyControl.Right);
                    break;

                //если нажата стрелочка вниз, то коордианта вертолётика смещается вниз
                case Key.Down:
                    viewModel.KeyUser(KeyControl.Down);
                    break;

                //если нажата стрелочка влево, то коордианта вертолётика смещается влево
                case Key.Left:
                    viewModel.KeyUser(KeyControl.Left);
                    break;

                //если нажата W, то вертолётик смещается вверх-вправо
                case Key.W:
                    viewModel.KeyUser(KeyControl.UpRight);
                    break;

                //если нажата Q, то вертолётик смещается вверх-влево
                case Key.Q:
                    viewModel.KeyUser(KeyControl.UpLeft);
                    break;

                //если нажата S, то вертолётик смещается вниз-вправо
                case Key.S:
                    viewModel.KeyUser(KeyControl.DownRight);
                    break;

                //если нажата A, то вертолётик смещается вниз-влево
                case Key.A:
                    viewModel.KeyUser(KeyControl.DownLeft);
                    break;

                //если нажата клавиша Escape, то вызывается меню паузы
                case Key.Escape:
                    viewModel.KeyUser(KeyControl.Pause);
                    PauseMenu pause = new PauseMenu();

                    if (pause.ShowDialog() == true)
                    {

                    }
                    else
                    {
                        viewModel.KeyUser(KeyControl.Continue);
                    }
                    break;
                case Key.Space:
                    viewModel.KeyUser(KeyControl.Continue);
                    break;

                //если нажата клавиша Z, то применяется способность и вызывается её анимация
                case Key.Z:
                    viewModel.KeyUser(KeyControl.Pif);
                    break;

                //если нажата клавиша X, то применяется способность и вызывается её анимация
                case Key.X:
                    viewModel.KeyUser(KeyControl.Paf);
                    break;

                //если нажата клавиша C, то применяется способность и вызывается её анимация
                case Key.C:
                    viewModel.KeyUser(KeyControl.Napalm);
                    break;
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.A:
                case Key.Down:
                case Key.S:
                case Key.Left:
                case Key.Right:
                case Key.Q:
                case Key.Up:
                case Key.W:
                    viewModel.KeyUser(KeyControl.Stop);
                    break;
            }
        }
    }
}
