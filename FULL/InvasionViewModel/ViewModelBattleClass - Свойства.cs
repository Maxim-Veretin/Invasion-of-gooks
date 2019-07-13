using CommLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvasionViewModel
{
    public partial class ViewModelBattleClass : OnPropertyChangedClass
    {
        private bool _isShowExitGameMenu = false;
        private bool _isPauseKey = true;
        private bool _isEndGame;

        /// <summary>Показ меню выхода из игры</summary>
        public bool IsShowExitGameMenu
        {
            get => _isShowExitGameMenu;
            private set
            {
                _isShowExitGameMenu = value;
                OnPropertyChanged();
                PauseGame();
            }
        }
        /// <summary>Показ паузы в игре</summary>
        public bool IsPauseKey
        {
            get => _isPauseKey;
            private set
            {
                _isPauseKey = value;
                OnPropertyChanged();
                PauseGame();
            }
        }

        /// <summary>Метод задающий паузу в игре</summary>
        private void PauseGame()
        {
            if (ViewModelGameStaticProperty.SingularExemplar.IsPause = IsPauseKey || IsShowExitGameMenu || IsEndGame)
                model.GamePause();
            else
                model.GameContinue();

        }

        /// <summary>Игра закончена</summary>
        public bool IsEndGame
        {
            get => _isEndGame;
            private set
            {
                ViewModelGameStaticProperty.SingularExemplar.IsEnded = _isEndGame = value;
                OnPropertyChanged();
                if (IsEndGame)
                    ViewModelGameStaticProperty.SingularExemplar.IsStarted = false;
                PauseGame();
            }
        }
    }
}