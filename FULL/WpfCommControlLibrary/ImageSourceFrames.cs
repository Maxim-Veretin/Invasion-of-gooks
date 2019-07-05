using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfCommControlLibrary
{

    /// <summary>DependencyObject для изображения и списка кадров</summary>
    public class ImageSourceFrames : DependencyObject
    {

        /// <summary>Изображение для раскадровки</summary>
        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ImageSource), typeof(ImageSourceFrames), new PropertyMetadata(null));


        /// <summary>Последовательность кадров в изображении</summary>
        public Rect[] Frames
        {
            get { return (Rect[])GetValue(FramesProperty); }
            set { SetValue(FramesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Frames.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FramesProperty =
            DependencyProperty.Register(nameof(Frames), typeof(Rect[]), typeof(AnimationFrames), 
                new PropertyMetadata(null, null, (CoerceValueCallback) coerceValueCallback));

        private static object coerceValueCallback(DependencyObject d, object baseValue)
        {
            if (baseValue is IEnumerable<Rect> ieVal)
                return ieVal.ToArray();
            return null;
        }

    }
}
