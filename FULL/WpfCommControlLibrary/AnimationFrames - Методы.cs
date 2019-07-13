using System.Windows;

namespace WpfCommControlLibrary
{
    /// <summary>Класс для анимации кадров из одного изображения</summary>
    public partial class AnimationFrames
    {
        /// <summary>Метод обратного вызова при изменении значения свойства Action (действие)</summary>
        private void ActionChangedCallback(AnimationFramesActionEnum newValue, AnimationFramesActionEnum oldValue)
        {
            switch (newValue)
            {
                case AnimationFramesActionEnum.Collapsed:
                    Visibility = Visibility.Collapsed;
                    break;
                case AnimationFramesActionEnum.Hidden:
                    Visibility = Visibility.Hidden;
                    break;
                case AnimationFramesActionEnum.Visible:
                    Visibility = Visibility.Visible;
                    break;
                case AnimationFramesActionEnum.Pause:
                    timer.Stop();
                    break;
                case AnimationFramesActionEnum.Play:
                    timer.Start();
                    break;
                case AnimationFramesActionEnum.Start:
                    timer.Stop();

                    /// Если источник кадров задан, то анимация начинается сразу.
                    /// Иначе анимация начнётся после загрузки источника
                    CurrentFrame = (isStarted = (SourceFrames == null)) ? -1 : 0;

                    Action = AnimationFramesActionEnum.Visible;
                    Action = AnimationFramesActionEnum.Play;
                    break;
                case AnimationFramesActionEnum.Stop:
                    timer.Stop();
                    Action = AnimationFramesActionEnum.Hidden;
                    CurrentFrame = -1;
                    break;
            }
        }

        /// <summary>Поле для отложенного запуска после загрузки</summary>
        private bool isStarted;

        /// <summary>Установка заданного кадра</summary>
        /// <param name="newValue">Номер устанавливаемого кадра</param>
        /// <param name="oldValue">Номер предыдущего кадра</param>
        private void CurrentFrameChangedCallback(int newValue, int oldValue)
        {
            if (oldValue >= 0 && newValue < 0)
                Action = AnimationFramesActionEnum.Stop;

            if (newValue >= 0 && newValue < SourceFrames.Frames.Length)
                ImageFrameBrush.Viewbox = SourceFrames.Frames[newValue];
        }
        /// <summary>Проверка нового значения текущего кадра</summary>
        /// <param name="newValue">Новое значение</param>
        /// <returns>Новое значение приведённое к допустимому диапазону</returns>
        private int CurrentFrameCoerceValueCallback(int newValue)
        {
            if (newValue < -1 || SourceFrames?.Frames == null || SourceFrames.Frames.Length < 1 || newValue >= SourceFrames.Frames.Length)
                return Replay ? 0 : -1;
            return newValue;
        }

        /// <summary>Смена кадра по такту таймера</summary>
        private void FrameTick()
        {
            CurrentFrame++;
        }


    }
}
