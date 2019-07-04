using CommLibrary;
using InvasionModel;
using InvasionViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Invasion_of_Gooks.View
{
    /// <summary>
    /// Логика взаимодействия для BattlePage.xaml
    /// </summary>
    public partial class BattlePage : Page
    {
        ViewModelBattleClass ViewModelBattle { get; }
        public BattlePage(ViewModelBattleClass viewModelBattle)
        {
            InitializeComponent();
            ViewModelBattle = viewModelBattle;
            DataContext = ViewModelBattle;
            viewModelBattle.SoundEvent += WarSky_SoundEvent;
            ViewModelGameStaticProperty.SingularExemplar.PropertyChanged += ViewModelBattle_PropertyChanged;
            PreviewKeyDown += BattlePage_KeyDown;
            PreviewKeyUp += BattlePage_KeyUp;
            Focusable = true;
            Loaded += BattlePage_Loaded;
            Unloaded += BattlePage_Unloaded;
        }

        private void BattlePage_Unloaded(object sender, RoutedEventArgs e)
        {
            ViewModelBattle.BreakGame();
        }

        private void ViewModelBattle_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.PropertyName) || e.PropertyName == nameof(ViewModelGameStaticProperty.IsPause))
                if (ViewModelGameStaticProperty.SingularExemplar.IsPause)
                    MediaPlayerEnum.Propeller1.Pause();
                else
                    MediaPlayerEnum.Propeller1.Continue();
        }

        private void BattlePage_Loaded(object sender, RoutedEventArgs e)
        {
            Focus();
            ViewModelBattle.StartGame();
        }

        /// <summary>Список нажатых клавиш</summary>
        private readonly List<KeyEventArgs> кeyDownList = new List<KeyEventArgs>();

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


        private void BattlePage_KeyUp(object sender, KeyEventArgs e)
        {
            кeyDownList.RemoveAll(item => item.Key==e.Key);
            if (кeyDownList.Count == 0)
                ViewModelBattle.KeyCommand.Execute(null);
            else
                BattlePage_KeyDown(this, кeyDownList.Last());
        }

        private Dictionary<SoundEnum, MediaPlayerEnum> soundPleers = new Dictionary<SoundEnum, MediaPlayerEnum>()
        {
            {SoundEnum.enemyDie, MediaPlayerEnum.Bang },
            {SoundEnum.gamerRocket, MediaPlayerEnum.Rocket },
            {SoundEnum.gamerShot, MediaPlayerEnum.SingleShot }
        };
        private void WarSky_SoundEvent(object sender, SoundEnum sound)
        {
            soundPleers[sound].Play();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);
        }
    }
}
