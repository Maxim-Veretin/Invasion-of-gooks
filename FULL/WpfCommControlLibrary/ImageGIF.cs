using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfCommControlLibrary
{
    //public class ImageGIF : System.Windows.Controls.Image
    //{


    //    public ImageGIF()
    //    {
    //        this.SourceUpdated += ImageGIF_SourceUpdated;
    //    }

    //    private void ImageGIF_SourceUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public new ImageSource Source
    //    {
    //        get { return (ImageSource)GetValue(SourceProperty); }
    //        set { SetValue(SourceProperty, value); }
    //    }

    //    // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
    //    public new static readonly DependencyProperty SourceProperty =
    //        DependencyProperty.Register("Source", typeof(ImageSource), typeof(ImageGIF), new PropertyMetadata(null));


    //    private static readonly BitmapImage SuccessfulImage = GetImage(Resources.SuccessfulIcon);
    //    private static readonly BitmapImage ErrorImage = GetImage(Resources.ErrorIcon);
    //    private static readonly BitmapImage InformationImage = GetImage(Resources.InformationIcon);
    //    private static readonly BitmapImage WarningImage = GetImage(Resources.WarningIcon);
    //    private static BitmapImage GetImage(System.Drawing.Image bm)
    //    {
    //        var ms = new MemoryStream();
    //        bm.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
    //        var image = new BitmapImage();
    //        image.BeginInit();
    //        ms.Seek(0, SeekOrigin.Begin);
    //        image.StreamSource = ms;
    //        image.EndInit();
    //        return image;
    //    }

    //}
}
