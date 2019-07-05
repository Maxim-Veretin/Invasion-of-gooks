using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfCommControlLibrary
{
    public partial class AnimationFrames : UserControl
    {

        private DispatcherTimer timer = new DispatcherTimer();

        public AnimationFrames()
        {
            //AddChild(RectangleProp);

            ImageFrameBrush = new ImageBrush
            {
                AlignmentX = AlignmentX.Left,
                AlignmentY = AlignmentY.Top,
                //Viewbox = rects[0] /*new Rect(0, 0, frameW, frameH)*/,
                ViewboxUnits = BrushMappingMode.Absolute,
                //ImageSource = ExplosionImage
            };
            Background = ImageFrameBrush;
            timer.Tick += Timer_Tick;

            Binding bind = new Binding(nameof(SourceFrames)+"."+nameof(SourceFrames.Source)) { Source = this };

            SetBinding(ImageFramesProperty, bind);

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            FrameTick();
            if (CurrentFrame > -1)
                timer.Start();
        }

        /// <summary>Изображение для раскадровки</summary>
        private ImageSource ImageFrames
        {
            get { return (ImageSource)GetValue(ImageFramesProperty); }
            set { SetValue(ImageFramesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageFrames.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty ImageFramesProperty =
            DependencyProperty.Register(nameof(ImageFrames), typeof(ImageSource), typeof(AnimationFrames),
                new PropertyMetadata(null, (PropertyChangedCallback)ImageFramesChangedCallback));

        private static void ImageFramesChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((AnimationFrames)d).ImageFrameBrush.ImageSource = (ImageSource)e.NewValue;
        }



        public ImageSourceFrames SourceFrames
        {
            get { return (ImageSourceFrames)GetValue(SourceFramesProperty); }
            set { SetValue(SourceFramesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SourceFrames.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceFramesProperty =
            DependencyProperty.Register(nameof(SourceFrames), typeof(ImageSourceFrames), typeof(AnimationFrames), new PropertyMetadata(null));




        /// <summary>Интервал между кадрами в миллисекундах</summary>
        public uint Interval
        {
            get { return (uint)GetValue(IntervalProperty); }
            set { SetValue(IntervalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Interval.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IntervalProperty =
            DependencyProperty.Register(nameof(Interval), typeof(uint), typeof(AnimationFrames),
                new PropertyMetadata(0u, (PropertyChangedCallback)IntervalChangedCallback));

        private static void IntervalChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((AnimationFrames)d).timer.Interval = TimeSpan.FromMilliseconds((uint)e.NewValue);
        }

        //new FrameworkPropertyMetadata(null, FramesChangedCallback));

        //private static void FramesChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    ICollection<Rect> newValue = (ICollection<Rect>)e.NewValue;
        //    if (newValue == null)
        //        ((AnimationFrames)d).CountFrames = -1;
        //    else
        //        ((AnimationFrames)d).CountFrames = newValue.Count;
        //}

        ///// <summary>Количество кадров</summary>
        //public int CountFrames
        //{
        //    get { return (int)GetValue(CountFramesProperty); }
        //    private set { SetValue(CountFramesKey, value); }
        //}

        //// Using a DependencyProperty as the backing store for CountFrames.  This enables animation, styling, binding, etc...
        //protected static readonly DependencyPropertyKey CountFramesKey =
        //    DependencyProperty.RegisterReadOnly("CountFrames", typeof(int), typeof(AnimationFrames), new PropertyMetadata(-1));


        //// Using a DependencyProperty as the backing store for CountFrames.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty CountFramesProperty = CountFramesKey.DependencyProperty;

        /// <summary>Действие</summary>
        public AnimationFramesActionEnum Action
        {
            get { return (AnimationFramesActionEnum)GetValue(ActionProperty); }
            set { SetValue(ActionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ActionEnum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActionProperty =
            DependencyProperty.Register(nameof(Action), typeof(AnimationFramesActionEnum), typeof(AnimationFrames),
                new FrameworkPropertyMetadata(AnimationFramesActionEnum.Hidden, ActionChangedCallback, (CoerceValueCallback)ActionCoerceValueCallback));

        private static object ActionCoerceValueCallback(DependencyObject d, object baseValue)
        {
            if (((AnimationFrames)d).ActionCallback == null)
                return baseValue;
            else
                return ((AnimationFrames)d).ActionCallback((AnimationFramesActionEnum)baseValue, ((AnimationFrames)d).Action);
        }

        private static void ActionChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((AnimationFrames)d).ActionChangedCallback((AnimationFramesActionEnum)e.NewValue, (AnimationFramesActionEnum)e.OldValue);
        }


        /// <summary>Номер текущего кадра.
        /// Кадры считаются от нуля.</summary>
        public int CurrentFrame
        {
            get => (int)GetValue(CurrentFrameProperty);
            set => SetValue(CurrentFrameProperty, value);
        }

        // Using a DependencyProperty as the backing store for CurrentFrame.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentFrameProperty =
            DependencyProperty.Register(nameof(CurrentFrame), typeof(int), typeof(AnimationFrames),
                new FrameworkPropertyMetadata(-1, (PropertyChangedCallback)CurrentFrameChangedCallback, (CoerceValueCallback)CurrentFrameCoerceValueCallback));

        private static object CurrentFrameCoerceValueCallback(DependencyObject d, object baseValue)
            => ((AnimationFrames)d).CurrentFrameCoerceValueCallback((int)baseValue);

        private static void CurrentFrameChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((AnimationFrames)d).CurrentFrameChangedCallback((int)e.NewValue, (int)e.OldValue);
        }


        /// <summary>Изображение текущего кадра.
        /// Неизменяемо!</summary>
        public ImageBrush ImageFrame => ((ImageBrush)GetValue(ImageFrameProperty)).Clone();

        /// <summary>Изображение текущего кадра.
        ///Изменяемо!</summary>
        protected ImageBrush ImageFrameBrush
        {
            get => (ImageBrush)GetValue(ImageFrameProperty);
            set => SetValue(ImageFramePropertyKey, value);
        }

        // Using a DependencyProperty as the backing store for ImageFrame.  This enables animation, styling, binding, etc...
        protected static readonly DependencyPropertyKey ImageFramePropertyKey =
            DependencyProperty.RegisterReadOnly("ImageFrame", typeof(ImageBrush), typeof(AnimationFrames), new PropertyMetadata(null));

        public static readonly DependencyProperty ImageFrameProperty = ImageFramePropertyKey.DependencyProperty;




        public bool Replay
        {
            get { return (bool)GetValue(ReplayProperty); }
            set { SetValue(ReplayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Replay.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ReplayProperty =
            DependencyProperty.Register("Replay", typeof(bool), typeof(AnimationFrames), new PropertyMetadata(false));



        public ActionCallbackHandler ActionCallback
        {
            get { return (ActionCallbackHandler)GetValue(ActionCallbackProperty); }
            set { SetValue(ActionCallbackProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ActionCallback.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActionCallbackProperty =
            DependencyProperty.Register("ActionCallback", typeof(ActionCallbackHandler), typeof(AnimationFrames), new PropertyMetadata(null));



    }

    public delegate AnimationFramesActionEnum ActionCallbackHandler(AnimationFramesActionEnum newValue, AnimationFramesActionEnum oldValue);
}
