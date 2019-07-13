using InvasionModel;
using System;
using System.Globalization;
using System.Windows.Data;
using WpfCommControlLibrary;

namespace Invasion_of_Gooks
{
    /// <summary>Конвертор к типу ExplosionClass для присоеденения делега обратного вызова на изменение дейстия анимации.
    /// Если действие меняется с Stop на Hidden, то здоровье ExplosionClass устанавливается = -1,
    /// что приводит к его удалению из списка объектов неба</summary>
    public class ExplosionAnimationFramesActionConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ExplosionClass explosion)
            {
                ActionCallbackHandler action = (AnimationFramesActionEnum newValue, AnimationFramesActionEnum oldValue)
                    => {
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
