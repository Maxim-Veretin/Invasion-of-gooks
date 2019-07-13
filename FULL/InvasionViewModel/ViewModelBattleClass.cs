using CommLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using InvasionModel;
using System.Threading.Tasks;
using System.Threading;

namespace InvasionViewModel
{
    public partial class ViewModelBattleClass : OnPropertyChangedClass
    {

        /// <summary>Ширина неба</summary>
        public double SkyWidth { get; set; }
        /// <summary>Высота неба</summary>
        public double SkyHeight { get; set; }

        /// <summary>Модель</summary>
        private readonly ModelClass model = new ModelClass();

        private GamerClass _gamer;
        private ObservableCollection<UFOClass> _uFOitems;
        /// <summary>Объекты неба</summary>
        public ObservableCollection<UFOClass> UFOitems { get => _uFOitems; private set { _uFOitems = value; OnPropertyChanged(); } }
        /// <summary>Объект Игрок</summary>
        public GamerClass Gamer { get => _gamer; private set { _gamer = value; OnPropertyChanged(); } }

        /// <summary>Небо</summary>
        public Sky warSky;

        /// <summary>Настройки</summary>
        public SkySetting Setting { get; }


        private void SaveResoult()
        {
            ModelDataBaseClass.Save(new DataGamer(ModelDataBaseClass.GamerName, warSky.Score));
        }


        /// <summary>Конец игры</summary>
        /// <param name="sky"></param>
        /// <param name="endGame"></param>
        /// <remarks>Игра заканчивается с паузой в 1 секунду,
        /// чтобы успела закончится анимация последнего взрыва</remarks>
        private async void WarSky_EndGameEventAsync(Sky sky, EndGameEnum endGame)
        {
            await Task.Run(() => Thread.Sleep(1000));
            IsEndGame = true;
            IsVictory = endGame == EndGameEnum.Win;
        }

        /// <summary>Перенаправление звуковых событий слушателям</summary>
        /// <param name="sender"></param>
        /// <param name="sound"></param>
        private void WarSky_SoundEvent(object sender, SoundEnum sound)
        {
            OnSound(sound);
        }

        /// <summary>Обработка действий из игры</summary>
        /// <param name="action">Действие</param>
        /// <remarks>Большая часть действий перенаправляется в Модель</remarks>
        public void KeyUser(KeyControl action)
        {
            switch (action)
            {
                case KeyControl.Pause:
                    //IsStop = true;
                    model.GamePause();
                    break;
                case KeyControl.Continue:
                    //IsStop = false;
                    model.GameContinue();
                    break;

                case KeyControl.Up:
                case KeyControl.Right:
                case KeyControl.Down:
                case KeyControl.Left:
                case KeyControl.Stop:
                case KeyControl.UpRight:
                case KeyControl.UpLeft:
                case KeyControl.DownRight:
                case KeyControl.DownLeft:
                case KeyControl.Pif:
                case KeyControl.Paf:
                    model.SetAction((ActionEnum)action);
                    break;
            }
        }

        public RelayCommand SaveResultCommand { get; }
    }

    /// <summary>Перечисление действий</summary>
    public enum KeyControl
    {
        Up,
        Down,
        Left,
        Right,
        Stop,
        UpRight,
        UpLeft,
        DownRight,
        DownLeft,
        Pif,
        Paf,
        Napalm,
        Pause,
        Continue,
    }
}
