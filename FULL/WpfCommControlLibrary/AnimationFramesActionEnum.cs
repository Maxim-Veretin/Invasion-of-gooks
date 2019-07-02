namespace WpfCommControlLibrary
{
    /// <summary>Перечисление действий для типа AnimationFrames</summary>
    /// <remarks>Collapsed - сворачивание.
    /// Hidden - скрытие.
    /// Start - начинает покадровую анимацию с первого кадра.
    /// Pause - останавливаете анимацию на текушем кадре
    /// Play - продолжает анимацию с текущего кадра
    /// Stop - прекращает анимацию и скрывает элемент</remarks>
    public enum AnimationFramesActionEnum
    {
        /// <summary>Свернуть</summary>
        Collapsed,
        /// <summary>Скрыть</summary>
        Hidden,
        /// <summary>Показать</summary>
        Visible,
        /// <summary>Начать сначала</summary>
        Start,
        /// <summary>Продолжить</summary>
        Play,
        /// <summary>Пауза</summary>
        Pause,
        /// <summary>Остановить</summary>
        Stop

    }
}
