using InvasionModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WpfCommControlLibrary;

namespace Invasion_of_Gooks
{
    public class ExplosionAnimationFramesActionConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ExplosionClass explosion)
            {
                ActionCallbackHandler action = (AnimationFramesActionEnum newValue, AnimationFramesActionEnum oldValue)
                    =>
                {
                    if (oldValue == AnimationFramesActionEnum.Stop && newValue == AnimationFramesActionEnum.Hidden)
                        explosion.Health=-1;
                    return newValue;
                };
                return action;
            }
            throw new ArgumentException();
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
