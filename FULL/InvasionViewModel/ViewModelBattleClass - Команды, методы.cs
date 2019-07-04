using CommLibrary;
using InvasionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace InvasionViewModel
{
    public partial class ViewModelBattleClass : OnPropertyChangedClass
    {
        private RelayCommand _keyCommand;
        private RelayCommand _exitBattleCommand;

        /// <summary>Команда обработки событий клавиатуры</summary>
        /// <remarks>Команда предназначена для обработки нажаьтий клавиш, 
        /// в параметре команды должна передаваться нажатая клавиша в типе System.Windows.Input.Key</remarks>
        public RelayCommand KeyCommand => _keyCommand ?? (_keyCommand = new RelayCommand(KeyMetod, KeyCanMetod));

        private bool KeyCanMetod(object parameter)
        {
            return parameter is Key;
        }

        private void KeyMetod(object parameter)
        {

            if (parameter is Key key)
            {
                switch (key)
                {
                    case Key.Space:
                        IsPauseKey ^= true;
                        break;
                    case Key.Escape:
                        if (ExitGameMenuCanMetod(Visibility.Visible)) ExitGameMenuMetod(Visibility.Visible);
                        break;
                }
            }
        }

        public RelayCommand ExitGameCommand;
        private RelayCommand _exitGameMenuCommand;

        public RelayCommand ExitBattleCommand => _exitBattleCommand ?? (_exitBattleCommand = new RelayCommand(ExitBattleMetod, ExitBattleCanMetod));

        private bool ExitBattleCanMetod(object parameter) => ExitGameCommand == null ? true : ExitGameCommand.CanExecute(parameter);

        private void ExitBattleMetod(object parameter)
        {
            if (ExitGameCommand != null)
                ExitGameCommand.Execute(parameter);
        }

        public RelayCommand ExitGameMenuCommand => _exitGameMenuCommand ?? (_exitGameMenuCommand = new RelayCommand(ExitGameMenuMetod, ExitGameMenuCanMetod));

        private bool ExitGameMenuCanMetod(object parameter)
        {
            if (parameter != null
                && (parameter is Visibility visibility || Enum.TryParse(parameter.ToString(), out visibility)))
                return visibility == Visibility.Visible ^ IsShowExitGameMenu;
            return false;
        }

        private void ExitGameMenuMetod(object parameter)
        {
            if (parameter is Visibility visibility || Enum.TryParse(parameter.ToString(), out visibility))
                IsShowExitGameMenu = visibility == Visibility.Visible;
        }

        /// <summary>Событие вызова звука</summary>
        public event SoundHandler SoundEvent;

        /// <summary>Вспомогательный метод для вызова события звука</summary>
        /// <param name="sound"></param>
        private void OnSound(SoundEnum sound) => SoundEvent?.Invoke(this, sound);

        public void StartGame()
        {
            IsPauseKey = true;
            IsShowExitGameMenu = false;
            if (warSky != null)
            {
                warSky.SoundEvent -= WarSky_SoundEvent;
                warSky.EndGameEvent -= WarSky_EndGameEvent;
                warSky.ExplosionEvent -= WarSky_ExplosionEvent;
            }
            model.GameStart();
            warSky = model.WarSky;

            UFOitems = warSky.UFOitems;
            //Gamer = warSky.Gamer;
            SkyWidth = warSky.Width;
            SkyHeight = warSky.Heidht;

            warSky.SoundEvent += WarSky_SoundEvent;
            warSky.EndGameEvent += WarSky_EndGameEvent;
            warSky.ExplosionEvent += WarSky_ExplosionEvent;
        }
        public void BreakGame()
        {
            model.GameBreak();
        }
    }
}
