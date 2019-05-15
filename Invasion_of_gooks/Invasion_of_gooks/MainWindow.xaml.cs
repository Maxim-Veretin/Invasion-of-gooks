using System;
using System.Windows;
using System.Windows.Media;
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

        public MainWindow()
        {
            InitializeComponent();
            Timer = new System.Windows.Threading.DispatcherTimer();
            Timer.Tick += new EventHandler(DispatcherTimer_Tick);
            Timer.Interval = new TimeSpan(0, 0, 1);
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            date = date.AddSeconds(1);
            stopwatch.Content = date.Hour.ToString() + " : " + date.Minute.ToString() + " : " + date.Second.ToString();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        { // перенести в main
            //запуск отсчёта
            Timer.Start();

            ////создание объекта многоугольник
            //Polygon myPolygon = new Polygon
            //{
            //    //установка цвета обводки, цвета заливки и толщины обводки
            //    Stroke = Brushes.Black,
            //    Fill = Brushes.LightSeaGreen,
            //    StrokeThickness = 2,
            //    //позиционирование объекта
            //    HorizontalAlignment = HorizontalAlignment.Left,
            //    VerticalAlignment = VerticalAlignment.Center
            //};
            ////создание точек многоугольника
            //Point Point1 = new Point(50, 0);
            //Point Point2 = new Point(100, 50);
            //Point Point3 = new Point(100, 100);
            //Point Point4 = new Point(0, 100);
            //Point Point5 = new Point(0, 50);
            ////Point Point1 = new Point(0, 0);
            ////Point Point2 = new Point(100, 0);
            ////Point Point3 = new Point(100, 50);
            ////Point Point4 = new Point(50, 100);
            ////Point Point5 = new Point(0, 50);
            ////создание и заполнение коллекции точек
            //PointCollection myPointCollection = new PointCollection
            //{
            //    Point1,
            //    Point2,
            //    Point3,
            //    Point4,
            //    Point5
            //};
            ////установка коллекции точек в объект многоугольник
            //myPolygon.Points = myPointCollection;
            ////добавление многоугольника в сцену
            //scene.Children.Add(myPolygon);

            Line vertL = new Line();
            vertL.X1 = 10;
            vertL.Y1 = 150;
            vertL.X2 = 10;
            vertL.Y2 = 10;
            vertL.Stroke = Brushes.Black;
            grid1.Children.Add(vertL);
            Line horL = new Line();
            horL.X1 = 10;
            horL.X2 = 150;
            horL.Y1 = 150;
            horL.Y2 = 150;
            horL.Stroke = Brushes.Black;
            grid1.Children.Add(horL);
            for (byte i = 2; i < 14; i++)
            {
                Line a = new Line();
                a.X1 = i * 10;
                a.X2 = i * 10;
                a.Y1 = 155;
                a.Y2 = 145;
                a.Stroke = Brushes.Black;
                grid1.Children.Add(a);
            }
            for (byte i = 2; i < 14; i++)
            {
                Line a = new Line();
                a.X1 = 5;
                a.X2 = 15;
                a.Y1 = i * 10;
                a.Y2 = i * 10;
                a.Stroke = Brushes.Black;
                grid1.Children.Add(a);
            }
            Polyline vertArr = new Polyline();
            vertArr.Points = new PointCollection();
            vertArr.Points.Add(new Point(5, 15));
            vertArr.Points.Add(new Point(10, 10));
            vertArr.Points.Add(new Point(15, 15));
            vertArr.Stroke = Brushes.Black;
            grid1.Children.Add(vertArr);
            Polyline horArr = new Polyline();
            horArr.Points = new PointCollection();
            horArr.Points.Add(new Point(145, 145));
            horArr.Points.Add(new Point(150, 150));
            horArr.Points.Add(new Point(145, 155));
            horArr.Stroke = Brushes.Black;
            grid1.Children.Add(horArr);
        }
    }
}