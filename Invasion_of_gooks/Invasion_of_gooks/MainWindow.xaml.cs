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

namespace Invasion_of_gooks
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer Timer;
        DateTime date = new DateTime();

        //координаты вертолёта 
        public int x = 633;
        public int y = 500;
        public Polygon helicopter = new Polygon();
        public MainWindow()
        {
            InitializeComponent();

            //таймер
            Timer = new System.Windows.Threading.DispatcherTimer();
            Timer.Tick += new EventHandler(DispatcherTimer_Tick);
            Timer.Interval = new TimeSpan(0, 0, 1);


            //создание объекта вертолёта


            //установка цвета обводки, цвета заливки и толщины обводки
            helicopter.Stroke = Brushes.Black;
            helicopter.Fill = Brushes.Green;
            helicopter.StrokeThickness = 2;
            //позиционирование объекта
            helicopter.HorizontalAlignment = HorizontalAlignment.Left;
            helicopter.VerticalAlignment = VerticalAlignment.Center;
            
            //создание точек многоугольника
            Point Point1 = new Point(x, y - 50);
            Point Point2 = new Point(x + 50, y);
            Point Point3 = new Point(x + 50, y + 50);
            Point Point4 = new Point(x - 50, y + 50);
            Point Point5 = new Point(x - 50, y);
            //создание и заполнение коллекции точек
            PointCollection helicopterCollection = new PointCollection
            {
                Point1,
                Point2,
                Point3,
                Point4,
                Point5
            };
            //установка коллекции точек в объект многоугольник
            helicopter.Points = helicopterCollection;
            //добавление многоугольника в сцену
            can.Children.Add(helicopter);
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            //запуск отсчёта
            Timer.Start();

            //делаем элементы меню неактивными
            Settings.IsEnabled = false;
            Start.IsEnabled = false;
            Exit.IsEnabled = false;
            upgrades.IsEnabled = false;
            achievements.IsEnabled = false;

            //делаем элементы меню невидимыми
            Settings.Visibility = Visibility.Collapsed;
            Start.Visibility = Visibility.Collapsed;
            Exit.Visibility = Visibility.Collapsed;
            upgrades.Visibility = Visibility.Collapsed;
            achievements.Visibility = Visibility.Collapsed;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            //выход из игры
            Environment.Exit(0);
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            date = date.AddSeconds(1);
            stopwatch.Content = date.Hour.ToString() + " : " + date.Minute.ToString() + " : " + date.Second.ToString();
            can.Children.Remove(helicopter);
            //установка цвета обводки, цвета заливки и толщины обводки
            helicopter.Stroke = Brushes.Black;
            helicopter.Fill = Brushes.Green;
            helicopter.StrokeThickness = 2;
            //позиционирование объекта
            helicopter.HorizontalAlignment = HorizontalAlignment.Left;
            helicopter.VerticalAlignment = VerticalAlignment.Center;

            //создание точек многоугольника
            Point Point1 = new Point(x, y - 50);
            Point Point2 = new Point(x + 50, y);
            Point Point3 = new Point(x + 50, y + 50);
            Point Point4 = new Point(x - 50, y + 50);
            Point Point5 = new Point(x - 50, y);
            //создание и заполнение коллекции точек
            PointCollection helicopterCollection = new PointCollection
            {
                Point1,
                Point2,
                Point3,
                Point4,
                Point5
            };
            //установка коллекции точек в объект многоугольник
            helicopter.Points = helicopterCollection;
            //добавление многоугольника в сцену
            can.Children.Add(helicopter);




        }
        private void Key_Down(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                //если нажата стрелочка вверх, то коордианта вертолётика смещается вверх 
                case Key.Up:
                    helicopter.RenderTransform = new TranslateTransform(0, -10);
                    y -= 10;
                    break;

                //если нажата стрелочка вправо, то коордианта вертолётика смещается вправо 
                case Key.Right:
                    helicopter.RenderTransform = new TranslateTransform(+10, 0);
                    x += 10;
                    break;

                //если нажата стрелочка вниз, то коордианта вертолётика смещается вниз 
                case Key.Down:
                    helicopter.RenderTransform = new TranslateTransform(0, +10);
                    y += 10;
                    break;

                //если нажата стрелочка влево, то коордианта вертолётика смещается влево 
                case Key.Left:
                    helicopter.RenderTransform = new TranslateTransform(-10, 0);
                    x -= 10;
                    break;

                //если нажата клавиша Escape, то вызывается меню паузы 
                //case Key.Escape:
                //    PauseMenu pause = new PauseMenu();
                //    Timer.Stop();
                //    if (pause.ShowDialog() == true)
                //    {

                //    }
                //    else
                //    {
                //        Timer.Start();
                //    }
                //    break;

                //если нажата клавиша Z, то применяется способность и вызывается её анимация 
                case Key.Z:
                    break;

                //если нажата клавиша X, то применяется способность и вызывается её анимация 
                case Key.X:
                    break;

                //если нажата клавиша C, то применяется способность и вызывается её анимация 
                case Key.C:
                    break;
            }
        }
    }
}//доброе утро я только что проснулся