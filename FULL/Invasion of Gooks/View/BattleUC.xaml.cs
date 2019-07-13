using CommLibrary;
using InvasionModel;
using InvasionViewModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Invasion_of_Gooks.View
{
    /// <summary>
    /// Логика взаимодействия для BattlePage.xaml
    /// </summary>
    public partial class BattleUC : Grid
    {
        ViewModelBattleClass ViewModelBattle { get; set; }
        public BattleUC()
        {
            DataContextChanged += StartUC_DataContextChanged;
            InitializeComponent();
            //ViewModelBattle = viewModelBattle;
            //DataContext = ViewModelBattle;

            /// Отслеживание глобальных свойств
            ViewModelGameStaticProperty.SingularExemplar.PropertyChanged += ViewModelBattle_PropertyChanged;

            /// Отслеживание нажатий клавиш
            PreviewKeyDown += BattlePage_KeyDown;
            PreviewKeyUp += BattlePage_KeyUp;
            //Focusable = true;

            /// Отслеживание загрузки отображения игры
            Loaded += BattlePage_Loaded;
            Unloaded += BattlePage_Unloaded;

        }

        /// <summary>Обработка изменения контекста данных</summary>
        private void StartUC_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (ViewModelBattle != null)
                ViewModelBattle.SoundEvent -= WarSky_SoundEvent;
            ViewModelBattle = e.NewValue as ViewModelBattleClass;

            /// Озвучка звуковых событий из ViewModelBattle
            ViewModelBattle.SoundEvent += WarSky_SoundEvent;
        }


        /// <summary>Обработчик загрузки отображения игры</summary>
        private void BattlePage_Loaded(object sender, RoutedEventArgs e)
        {
            /// Получение фокуса для "отлова" нажатий клавиш
            Focus();
            /// Запуск VM игры
            ViewModelBattle.StartGame();
        }

        /// <summary>Обработчик выгрузки отображения игры</summary>
        private void BattlePage_Unloaded(object sender, RoutedEventArgs e)
        {
            /// Прерывание VM игры
            ViewModelBattle.BreakGame();
        }

        /// <summary>Обработчик события паузы в игре</summary>
        /// <param name="sender">Источник сбытия</param>
        /// <param name="e">Свойство</param>
        private void ViewModelBattle_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.PropertyName) || e.PropertyName == nameof(ViewModelGameStaticProperty.IsPause))
                /// Если пауза, то звук отключается. Иначе - включается.
                if (ViewModelGameStaticProperty.SingularExemplar.IsPause)
                    MediaPlayerEnum.Propeller1.Pause();
                else
                    MediaPlayerEnum.Propeller1.Continue();
        }

        /// <summary>Список нажатых клавиш</summary>
        private readonly List<KeyEventArgs> кeyDownList = new List<KeyEventArgs>();

        /// <summary>Обработка нажатия клавиш</summary>
        private void BattlePage_KeyDown(object sender, KeyEventArgs e)
        {
            кeyDownList.Add(e);
            /// Обработка двойных нажатий
            if (e.Key == Key.Up || e.Key == Key.Right || e.Key == Key.Down || e.Key == Key.Left)
            {
                KeyboardDevice keyBoard = e.KeyboardDevice;
                bool up = keyBoard.GetKeyStates(Key.Up).HasFlag(KeyStates.Down);
                bool right = keyBoard.GetKeyStates(Key.Right).HasFlag(KeyStates.Down);
                bool down = keyBoard.GetKeyStates(Key.Down).HasFlag(KeyStates.Down);
                bool left = keyBoard.GetKeyStates(Key.Left).HasFlag(KeyStates.Down);

                Key keyKey = 0;
                if (e.Key == Key.Up && (right || left))
                    keyKey = right ? Key.NumPad9 : Key.NumPad7;
                else if (e.Key == Key.Down && (right || left))
                    keyKey = right ? Key.NumPad3 : Key.NumPad1;
                else if (e.Key == Key.Right && (up || down))
                    keyKey = up ? Key.NumPad9 : Key.NumPad3;
                else if (e.Key == Key.Left && (up || down))
                    keyKey = up ? Key.NumPad7 : Key.NumPad1;
                if (keyKey != 0)
                {
                    ViewModelBattle.KeyCommand.CommandExecute(keyKey);
                    return;
                }
            }
            ViewModelBattle.KeyCommand.CommandExecute(e.Key);
        }


        /// <summary>Обработка отпускания клавиш</summary>
        private void BattlePage_KeyUp(object sender, KeyEventArgs e)
        {
            кeyDownList.RemoveAll(item => item.Key == e.Key);
            if (кeyDownList.Count == 0)
                ViewModelBattle.KeyCommand.Execute(null);
            else
                BattlePage_KeyDown(this, кeyDownList.Last());
        }

        /// <summary>Словарь соответсвия звуковых событий заданным плеерам</summary>
        private Dictionary<SoundEnum, MediaPlayerEnum> soundPleers = new Dictionary<SoundEnum, MediaPlayerEnum>()
        {
            {SoundEnum.enemyDie, MediaPlayerEnum.Bang },
            {SoundEnum.gamerRocket, MediaPlayerEnum.Rocket },
            {SoundEnum.gamerShot, MediaPlayerEnum.SingleShot }
        };
        /// <summary>Обработчик звуковых событий</summary>
        /// <param name="sender">Источник события</param>
        /// <param name="sound">Значение события</param>
        private void WarSky_SoundEvent(object sender, SoundEnum sound)
        {
            /// Запуск плеера соответсвующего звуковому событию
            soundPleers[sound].Play();
        }
    }
}
