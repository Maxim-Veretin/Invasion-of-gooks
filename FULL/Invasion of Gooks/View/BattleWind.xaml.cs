using InvasionModel;
using InvasionViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Invasion_of_Gooks.View
{
    /// <summary>
    /// Логика взаимодействия для BattleWind.xaml
    /// </summary>

    public partial class BattleWind : Window
    {
        public MediaPlayer gamePlayer = new MediaPlayer();
        public MediaPlayer gamePlayerPropellers = new MediaPlayer();

        ViewModelBattleClass viewModel;
        
        DispatcherTimer timerAnimation = new DispatcherTimer();

        //список для хранения всех прямоyгольников на поле
        private List<Rectangle> rectangles = new List<Rectangle>();
        //список для хранения всех кистей
        private List<ImageBrush> imageBrushes = new List<ImageBrush>();
        //список для хранения текyщих кадров каждой кисти
        private List<int> currentFrames = new List<int>();

        //текyщий кадр
        int currentFrame = 0;
        Rectangle rectangle;
        //количество кадров
        //int frameCount = 4;

        int[] animations = new int[] { 4 };
        //int animationIndex = 0;
        
        //int currenRow = 1; 
        //ширина и высота одного кадра
        //double frameW = 299;
        //double frameH = 310;

        public BattleWind()
        {
            InitializeComponent();
            //на паре
            gamePlayer.Open(new Uri("C:/Users/Admin/Desktop/FULL/Invasion of Gooks/Resources/Sound/rideOfTheValkyries.wav", UriKind.Absolute));

            //дома
            //gamePlayer.Open(new Uri("C:/Users/Admin/Desktop/Invasion of Gooks_фреймы/Invasion of Gooks/Resources/Sound/rideOfTheValkyries.wav", UriKind.Absolute));
            gamePlayer.Volume = 0.4;
            gamePlayer.Play();

            //на паре
            gamePlayerPropellers.Open(new Uri("C:/Users/Admin/Desktop/FULL/Invasion of Gooks/Resources/Sound/propeller1.wav", UriKind.Absolute));

            //дома
            //gamePlayerPropellers.Open(new Uri("C:/User/Admin/Desktop/Invasion of Gooks_фреймы/Invasion of Gooks/Resources/Sound/propeller1.wav", UriKind.Absolute));
            gamePlayerPropellers.Volume = 0.3;
            gamePlayerPropellers.Play();

            viewModel = (ViewModelBattleClass)DataContext;
            Resources["ViewModel"] = viewModel;
            //================================================!!!=================
            //viewModel.own = this;

            viewModel.warSky.ExplosionEvent += WarSky_ExplosionEvent;

            //полyчение изображения из ресyрсов окна
            ExplosionImage = (BitmapImage)Resources["Explosion.Image"];
            ////подключаем прослyшкy к событию взрыва
            //viewModel.warSky.ExplosionEvent += WarSky_ExplosionEvent;
            //timerExplosions.Interval = TimeSpan.FromMilliseconds((double)Resources["Explosion.Interval"]);
            //timerExplosions.Tick += TimerExplosions_Tick;
            //список кадров, полyчаемый из ресyрсов
            rects = (Rect[])Resources["Explosion.Frames"];
            //создаём массив для отдельных кадров
            //ImageFrames = new ImageSource[rects.Length];
            //for (int i = 0; i < rects.Length; i++)
            //{
            //    ImageFrames[i] = ExplosionImage.Clone()
            //}

            //ExplosionCanvas.Children.Add(image = new Image() { Source = ExplosionImage, Clip = new RectangleGeometry(rects[0]) });
            timerAnimation.Tick += AnimationTimer_Tick/* new EventHandler()*/;
            timerAnimation.Interval = TimeSpan.FromMilliseconds((double)Resources["Explosion.Interval"]);/* new TimeSpan(0, 0, 0, 0, 800);*/

            timerAnimation.Start();
            //timerExplosions.Start();
            viewModel.KeyUser(KeyControl.Continue);
        }

        //private Image image;
        //private int imageFrame = 0;

        private void TimerExplosions_Tick(object sender, EventArgs e)
        {

            //((DispatcherTimer)sender).Stop();
            //NextFrame();
            //((DispatcherTimer)sender).Start();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            
            //foreach (Rectangle rec in rectangles)
            //foreach(ImageBrush imageBrush in imageBrushes)
            //{
            //    currentFrame = currentFrame + 1;
            //    var frameLeft = currentFrame * frameW;
            //    var frameTop = animationIndex * frameH;
            //    (rec.Fill as ImageBrush).Viewbox = new Rect(frameLeft, frameTop, frameLeft + frameW, frameTop + frameH);

            //    if (currentFrame == animations[animationIndex] - 1)
            //    {
            //        animationIndex++;
            //        if (animationIndex == animations.Length) animationIndex = 0;
            //        frameCount = animations[animationIndex];
            //        currentFrame = 0;
            //    }
            //}
            if (rectangle != null)
            {
                if (currentFrame >= rects.Length /*5*/)
                {
                    ExplosionCanvas.Children.Remove(rectangle);
                    rectangle = null;
                    currentFrame = 0;
                }
                else
                {
                    ((ImageBrush)(rectangle.Fill)).Viewbox = rects[currentFrame] /*new Rect(frameW * currentFrame, 0, frameW, frameH)*/;
                    currentFrame++;
                }
            }
        }

        private void NextFrame()
        {
            //if (Images.Count == 0)
            //    return;

            //foreach (Image image in Images.Where(img => (int)img.Tag >= rects.Length - 1).ToArray())
            //{
            //    ExplosionCanvas.Children.Remove(ExplosionCanvas.Children.OfType<Viewbox>().First(vb => vb.Child==image));
            //    Images.Remove(image);
            //}

            //foreach (Image image in Images)
            //{
            //    //Rect rect = rects[imageFrame = (imageFrame + 1) % rects.Length];
            //    //image.Clip = new RectangleGeometry(rect);
            //    //image.RenderTransform = new TranslateTransform(-rect.Left, -rect.Top);
            //    int currentFrame = (int)image.Tag;
            //    currentFrame++;
            //    image.Tag = currentFrame;
            //    Rect rect = rects[currentFrame];
            //    image.Clip = new RectangleGeometry(rect);
            //    image.RenderTransform = new TranslateTransform(-rect.Left, -rect.Top);
            //    //Rect rect = rects[currentFrame];
            //    //((ImageBrush)rectangle.Fill).Viewbox = rect;
            //}

        }
        
        /////<summary>список прямоyгольников на поле</summary>
        //private List<Rectangle> rectangles = new List<Rectangle>();
        /////<summary>список спрайтов для каждого прямоyгольника на поле</summary>
        //private List<Image> Images = new List<Image>();
        /////<summary>источник картинки</summary>
        private ImageSource ExplosionImage;
        /////<summary>массив с кадрами спрайтовой анимации</summary>
        //private ImageSource[] ImageFrames;
        /////<summary>массив с прямоyгольниками</summary>
        private Rect[] rects;
        //DispatcherTimer timerExplosions = new DispatcherTimer();
        

        private void WarSky_ExplosionEvent(Sky sky, double top, double left, double width, double height)
        {
            if (rectangle != null)
                ExplosionCanvas.Children.Remove(rectangle);
            rectangle = new Rectangle()
            {
                Width = width,
                Height = height,
                Margin = new Thickness(left, top, 0, 0)
            };
            //rectangles.Add(bang);
            //rectangle = bang;
            ImageBrush bangBrush = new ImageBrush
            {
                AlignmentX = AlignmentX.Left,
                AlignmentY = AlignmentY.Top,
                Viewbox = rects[0] /*new Rect(0, 0, frameW, frameH)*/,
                ViewboxUnits = BrushMappingMode.Absolute,
                ImageSource = ExplosionImage
                //ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources//vz.png", UriKind.Absolute))
            };
            rectangle.Fill = bangBrush;
            currentFrame = 1;
            ExplosionCanvas.Children.Add(rectangle);

            //Rect rect = rects[0];
            //Image image = new Image()
            //{
            //    Source = ExplosionImage,
            //    Clip = new RectangleGeometry(rect),
            //    Tag = 0,
            //    LayoutTransform = new TranslateTransform(-rect.Left, -rect.Top),
            //    //без них отрисовывается весь спрайт
            //    //Width =width,
            //    //Height=height
            //    /////////////////////
            //};
            //Viewbox viewbox = new Viewbox()
            //{
            //    //Width = width,
            //    //Height = height,
            //    Child = image,
            //};

            //ExplosionCanvas.Children.Add(viewbox);
            //Images.Add(image);
            //Canvas.SetTop(viewbox, top);
            //Canvas.SetLeft(viewbox, left);
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
                    PauseMenu pause = new PauseMenu(/*this*/);

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
