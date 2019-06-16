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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Media;

namespace Invasion_of_gooks
{
    public partial class MainWindow : Window
    {
        //cоздание переменной таймер
        DispatcherTimer Timer;

        //создание переменной дата
        public DateTime date = new DateTime();

        //создание объектов прямоугольников
        public Rectangle helicopter = new Rectangle();
        public Rectangle projectile = new Rectangle();
        public Rectangle enemy = new Rectangle();

        //hp вертолёта
        public int hp_helicopter = 1;

        public int score = 0;

        //координаты вертолёта
        public int x = 630;
        public int y = 500;

        //координаты для отрисоки противников
        public int x_enemies;
        public int y_enemies = 300;

        //координаты снаряда
        public int y_projectile;

        //переменная, описывающая наличие снарядов на поле
        public bool have_projectile = false;
        public bool have_enemy = false;

        //даллары, мани, деньги, кэш
        public int dollars = 0;

        public SoundPlayer main_menu_player = new SoundPlayer();
        //public MediaPlayer main_menu_player = new MediaPlayer();

        //метод инициализации приложения
        public MainWindow()
        {
            InitializeComponent();
            
            //таймер
            Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(DispatcherTimer_Tick);
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 1);

            //отрисовываем вертолётик
            Add_Helicopter();

            //делаем его невидимым
            helicopter.Visibility = Visibility.Collapsed;

            MediaPlayer main_menu_player = new MediaPlayer();
            //main_menu_player.Open(new Uri(@"pack://application:,,,/sounds/The Black Angels - ComancheMoon.wav", UriKind.Absolute));
            main_menu_player.Open(new Uri(@"pack://application:,,,/sounds/The Black Angels - ComancheMoon.wav", UriKind.Absolute));
            main_menu_player.Balance = 0;
            main_menu_player.Volume = 1;
            main_menu_player.Play();
            //main_menu_player.Stream = Properties.Resources.The_Black_Angels_ComancheMoon;
            //main_menu_player.Play();
        }   

        //метод начала игры
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            //делаем элементы меню неактивными
            Start.IsEnabled = false;
            Exit.IsEnabled = false;
            upgrades.IsEnabled = false;
            achievements.IsEnabled = false;
            Sound_Off.IsEnabled = false;

            //делаем элементы меню невидимыми
            Start.Visibility = Visibility.Collapsed;
            Exit.Visibility = Visibility.Collapsed;
            upgrades.Visibility = Visibility.Collapsed;
            achievements.Visibility = Visibility.Collapsed;
            Sound_Off.Visibility = Visibility.Collapsed;

            //делаем вертолётик видимым
            helicopter.Visibility = Visibility.Visible;

            //добавляем новый фон
            ImageBrush fight_background = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/images/landtile.jpg", UriKind.Absolute))
            };
            fonMW.Source = fight_background.ImageSource;

            //останавливаем плеер меню
            main_menu_player.Stop();

            //создаём и запускаем плеер 2
            //MediaPlayer game_player = new MediaPlayer();
            //game_player.Open(new Uri(@"pack://application:,,,/sounds/Полёт валькирий.wav", UriKind.Absolute));
            //game_player.Balance = 0;
            //game_player.Volume = 1;
            //game_player.Play();
            SoundPlayer game_player = new SoundPlayer();
            game_player.Stream = Properties.Resources.Valkiry_fly;
            game_player.Play();

            x_enemies = x;

            //запускаем таймер
            Timer.Start();
        }

        //метод выхода из приложения
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            //выход из игры
            Environment.Exit(0);
            //Application.Current.Shutdown();
        }

        //метод, описывающий что будет происходить в каждый тик таймера
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            date = date.AddSeconds(1);
            stopwatch.Content = date.Hour.ToString() + " : " + date.Minute.ToString() + " : " + date.Second.ToString() + date.Millisecond.ToString();
            
            //удаление вертолётика со старыми координатами
            can.Children.Remove(helicopter);

            //отрисовка вертолётика с новыми координатами
            Add_Helicopter();

            if (date.Second / 5 == 0)
                have_enemy = true;

            Enemy(ref x);
            can.Children.Remove(enemy);
            

        }

        //метод отрисовки вертолётика
        private void Add_Helicopter()
        {
            //параметры выравнивания
            helicopter.HorizontalAlignment = HorizontalAlignment.Left;
            helicopter.VerticalAlignment = VerticalAlignment.Center;

            //размеры прямоугольника
            helicopter.Height = 100;
            helicopter.Width = 100;
            helicopter.Margin = new Thickness(x, y, 0, 0);

            //добавление объекта в сцену
            can.Children.Add(helicopter);

            //отрисовка вертолётика
            ImageBrush helicopter_image = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/images/helicopter_desert.png", UriKind.Absolute))
            };
            helicopter.Fill = helicopter_image;
        }

        //метод, включающий события нажатия клавиш
        private void Key_Down(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                //если нажата стрелочка вверх, то коордианта вертолётика смещается вверх
                case Key.Up:
                    if (y > 0)
                        y -= 10;
                    else
                        y = 0;
                    break;

                //если нажата стрелочка вправо, то коордианта вертолётика смещается вправо
                case Key.Right:
                    if ((x + 100) < 1361)
                        x += 10;
                    else
                        x = 1266;
                    break;
                    
                //если нажата стрелочка вниз, то коордианта вертолётика смещается вниз
                case Key.Down:
                    if ((y + 100) < 741)
                        y += 10;
                    else
                        y = 648;
                    break;

                //если нажата стрелочка влево, то коордианта вертолётика смещается влево
                case Key.Left:
                    if (x > 0)
                        x -= 10;
                    else
                        x = 0;
                    break;
                    
                //если нажата клавиша Escape, то вызывается меню паузы
                case Key.Escape:
                    PauseMenu pause = new PauseMenu();
                    Timer.Stop();
                    if (pause.ShowDialog()==true)
                    {

                    }
                    else
                    {
                        Timer.Start();
                    }
                    break;

                //если нажата клавиша Z, то применяется способность и вызывается её анимация
                case Key.Z:
                    Projectile(ref y);
                    break;
                    
                case Key.W:
                    //Enemy(ref x);

                    //DoubleAnimation bulletAnimation = new DoubleAnimation();
                    //bulletAnimation.From = y_bullet;
                    //bulletAnimation.To = 0;
                    //bulletAnimation.Duration = TimeSpan.FromSeconds(3);
                    //bullet.BeginAnimation(Canvas.TopProperty, bulletAnimation);
                    //can.Children.Remove(bullet);
                    break;

                //если нажата клавиша X, то применяется способность и вызывается её анимация
                case Key.X:
                    break;

                //если нажата клавиша C, то применяется способность и вызывается её анимация
                case Key.C:
                    break;
            }
        }

        //метод отрисовки пуль
        private void Projectile(ref int y_projectile)
        {
            Rectangle projectile = new Rectangle();

            //y_projectile = y;

            projectile.Fill = Brushes.Red;

            //параметры выравнивания
            projectile.HorizontalAlignment = HorizontalAlignment.Left;
            projectile.VerticalAlignment = VerticalAlignment.Center;

            //размеры прямоугольника
            projectile.Height = 10;
            projectile.Width = 10;
            projectile.Margin = new Thickness(x+45, y_projectile, 0, 0);

            //добавление объекта в сцену
            can.Children.Add(projectile);

            DoubleAnimationUsingPath projectileAnimation = new DoubleAnimationUsingPath();



            //DoubleAnimation bulletAnimation = new DoubleAnimation();
            //bulletAnimation.From = y_bullet;
            //bulletAnimation.To = 0;
            //bulletAnimation.Duration = TimeSpan.FromSeconds(3);
            //bullet.BeginAnimation(Button.WidthProperty, bulletAnimation);
            //can.Children.Remove(bullet);

            have_projectile = true;
            //MessageBox.Show("выстрел");
        }

        //метод обнаружения пересечений объектов
        public bool Intersection()
        {
            return false;
        }

        //метод отрисовки 3-х противников
        private void Enemy(ref int x_enemy)
        {
            if (have_enemy == true)
            {
                //параметры выравнивания
                enemy.HorizontalAlignment = HorizontalAlignment.Left;
                enemy.VerticalAlignment = VerticalAlignment.Center;

                //размеры прямоугольника
                enemy.Height = 100;
                enemy.Width = 100;
                enemy.Margin = new Thickness(x_enemy, y_enemies, 0, 0);

                //добавление объекта в сцену
                can.Children.Add(enemy);

                //отрисовка вертолётика
                ImageBrush helicopter_image = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/images/helicopter.jpg", UriKind.Absolute))
                };
                enemy.Fill = helicopter_image;
            }
            else
                MessageBox.Show("Лох пидр");
        }
    }
}