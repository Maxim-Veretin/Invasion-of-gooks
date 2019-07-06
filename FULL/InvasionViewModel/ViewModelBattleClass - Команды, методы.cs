using CommLibrary;
using InvasionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Collections.Immutable;

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

                    //если нажата стрелочка вверх, то коордианта вертолётика смещается вверх
                    case Key.NumPad8:
                    case Key.Up:
                        KeyUser(KeyControl.Up);
                        break;

                    //если нажата стрелочка вправо, то коордианта вертолётика смещается вправо
                    case Key.NumPad6:
                    case Key.Right:
                        KeyUser(KeyControl.Right);
                        break;

                    //если нажата стрелочка вниз, то коордианта вертолётика смещается вниз
                    case Key.NumPad2:
                    case Key.Down:
                        KeyUser(KeyControl.Down);
                        break;

                    //если нажата стрелочка влево, то коордианта вертолётика смещается влево
                    case Key.NumPad4:
                    case Key.Left:
                        KeyUser(KeyControl.Left);
                        break;

                    //если нажата W, то вертолётик смещается вверх-вправо
                    case Key.NumPad9:
                    case Key.PageUp:
                    case Key.W:
                        KeyUser(KeyControl.UpRight);
                        break;

                    //если нажата Q, то вертолётик смещается вверх-влево
                    case Key.NumPad7:
                    case Key.Home:
                    case Key.Q:
                        KeyUser(KeyControl.UpLeft);
                        break;

                    //если нажата S, то вертолётик смещается вниз-вправо
                    case Key.NumPad3:
                    case Key.PageDown:
                    case Key.S:
                        KeyUser(KeyControl.DownRight);
                        break;

                    //если нажата A, то вертолётик смещается вниз-влево
                    case Key.NumPad1:
                    case Key.End:
                    case Key.A:
                        KeyUser(KeyControl.DownLeft);
                        break;

                    //если нажата клавиша Z, то применяется способность и вызывается её анимация
                    case Key.Z:
                        KeyUser(KeyControl.Pif);
                        break;

                    //если нажата клавиша X, то применяется способность и вызывается её анимация
                    case Key.X:
                        KeyUser(KeyControl.Paf);
                        break;

                    //если нажата клавиша C, то применяется способность и вызывается её анимация
                    case Key.C:
                        KeyUser(KeyControl.Napalm);
                        break;
                }
            }
            else if (parameter == null)
            {
                KeyUser(KeyControl.Stop);
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
                //warSky.ExplosionEvent -= WarSky_ExplosionEvent;
            }
            model.GameStart();
            warSky = model.WarSky;

            UFOitems = warSky.UFOitems;
            Gamer = warSky.Gamer;
            SkyWidth = warSky.Width;
            SkyHeight = warSky.Heidht;

            warSky.SoundEvent += WarSky_SoundEvent;
            warSky.EndGameEvent += WarSky_EndGameEvent;
            //warSky.ExplosionEvent += WarSky_ExplosionEvent;
        }
        public void BreakGame()
        {
            model.GameBreak();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                //если нажата стрелочка вверх, то коордианта вертолётика смещается вверх
                case Key.Up:
                    KeyUser(KeyControl.Up);
                    break;

                //если нажата стрелочка вправо, то коордианта вертолётика смещается вправо
                case Key.Right:
                    KeyUser(KeyControl.Right);
                    break;

                //если нажата стрелочка вниз, то коордианта вертолётика смещается вниз
                case Key.Down:
                    KeyUser(KeyControl.Down);
                    break;

                //если нажата стрелочка влево, то коордианта вертолётика смещается влево
                case Key.Left:
                    KeyUser(KeyControl.Left);
                    break;

                //если нажата W, то вертолётик смещается вверх-вправо
                case Key.W:
                    KeyUser(KeyControl.UpRight);
                    break;

                //если нажата Q, то вертолётик смещается вверх-влево
                case Key.Q:
                    KeyUser(KeyControl.UpLeft);
                    break;

                //если нажата S, то вертолётик смещается вниз-вправо
                case Key.S:
                    KeyUser(KeyControl.DownRight);
                    break;

                //если нажата A, то вертолётик смещается вниз-влево
                case Key.A:
                    KeyUser(KeyControl.DownLeft);
                    break;

                ////если нажата клавиша Escape, то вызывается меню паузы
                //case Key.Escape:
                //    viewModel.KeyUser(KeyControl.Pause);
                //    PauseMenu pause = new PauseMenu(/*this*/);

                //    if (pause.ShowDialog() == true)
                //    {

                //    }
                //    else
                //    {
                //        viewModel.KeyUser(KeyControl.Continue);
                //    }
                //    break;
                //case Key.Space:
                //    viewModel.KeyUser(KeyControl.Continue);
                //    break;

                //если нажата клавиша Z, то применяется способность и вызывается её анимация
                case Key.Z:
                    KeyUser(KeyControl.Pif);
                    break;

                //если нажата клавиша X, то применяется способность и вызывается её анимация
                case Key.X:
                    KeyUser(KeyControl.Paf);
                    break;

                //если нажата клавиша C, то применяется способность и вызывается её анимация
                case Key.C:
                    KeyUser(KeyControl.Napalm);
                    break;
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.A:
                case Key.Down:
                case Key.S:
                case Key.Left:
                case Key.Right:
                case Key.Q:
                case Key.Up:
                case Key.W:
                    KeyUser(KeyControl.Stop);
                    break;
            }
        }

    }
}
