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

        private void BattlePage_KeyDown(object sender, KeyEventArgs e)
        {
            if (ViewModelBattle.KeyCommand.CanExecute(e.Key))
                ViewModelBattle.KeyCommand.Execute(e.Key);
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
