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

        public Polygon helicopter = new Polygon();
        public MainWindow()
        {
            InitializeComponent();

            //таймер
            Timer = new System.Windows.Threading.DispatcherTimer();
            Timer.Tick += new EventHandler(DispatcherTimer_Tick);
            Timer.Interval = new TimeSpan(0, 0, 1);

            Point helicoptercenter = new Point(683, 500);
            //создание объекта вертолёта


            //установка цвета обводки, цвета заливки и толщины обводки

            helicopter.Stroke = Brushes.Black;
            helicopter.Fill = Brushes.Green;
            helicopter.StrokeThickness = 2;
            //позиционирование объекта
            helicopter.HorizontalAlignment = HorizontalAlignment.Left;
            helicopter.VerticalAlignment = VerticalAlignment.Center;
            
            //создание точек многоугольника

            Point Point1 = new Point(helicoptercenter.X, helicoptercenter.Y - 50);
            Point Point2 = new Point(helicoptercenter.X + 50, helicoptercenter.Y);
            Point Point3 = new Point(helicoptercenter.X + 50, helicoptercenter.Y + 50);
            Point Point4 = new Point(helicoptercenter.X - 50, helicoptercenter.Y + 50);
            Point Point5 = new Point(helicoptercenter.X - 50, helicoptercenter.Y);
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
            helicopter.Visibility = Visibility.Collapsed;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            

            //делаем элементы меню неактивными
            Start.IsEnabled = false;
            Exit.IsEnabled = false;
            upgrades.IsEnabled = false;
            achievements.IsEnabled = false;

            //делаем элементы меню невидимыми
            Start.Visibility = Visibility.Collapsed;// функция сеттинг еще
            Exit.Visibility = Visibility.Collapsed;
            upgrades.Visibility = Visibility.Collapsed;
            achievements.Visibility = Visibility.Collapsed;
            //запуск отсчёта
            helicopter.Visibility = Visibility.Visible;
            Timer.Start();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            //выход из игры
            Environment.Exit(0);
        }

       
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            date = date.AddSeconds(1);
            stopwatch.Content = date.Hour.ToString() + " : " + date.Minute.ToString() + " : " + date.Second.ToString();
        }
    }
}