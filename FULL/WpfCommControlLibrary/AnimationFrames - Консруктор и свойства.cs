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
        /// <summary>Поле с таймером</summary>
        private DispatcherTimer timer = new DispatcherTimer();

        public AnimationFrames()
        {

            ImageFrameBrush = new ImageBrush
            {
                AlignmentX = AlignmentX.Left,
                AlignmentY = AlignmentY.Top,
                ViewboxUnits = BrushMappingMode.Absolute,
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


        /// <summary>Источник кадров</summary>
        public ImageSourceFrames SourceFrames
        {
            get { return (ImageSourceFrames)GetValue(SourceFramesProperty); }
            set { SetValue(SourceFramesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SourceFrames.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceFramesProperty =
            DependencyProperty.Register(nameof(SourceFrames), typeof(ImageSourceFrames), typeof(AnimationFrames), new PropertyMetadata(null, 
                (PropertyChangedCallback)SourceFramesChangedCallback));

        private static void SourceFramesChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (((AnimationFrames)d).isStarted)
                ((AnimationFrames)d).Action= AnimationFramesActionEnum.Start;
        }




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



        /// <summary>Циклический повтор анимации</summary>
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

    /// <summary>Делегат для обратного вызова при изменении действия</summary>
    /// <param name="newValue">Новое действие</param>
    /// <param name="oldValue">Старое действие</param>
    /// <returns>Действие которое надо установить</returns>
    public delegate AnimationFramesActionEnum ActionCallbackHandler(AnimationFramesActionEnum newValue, AnimationFramesActionEnum oldValue);
}
