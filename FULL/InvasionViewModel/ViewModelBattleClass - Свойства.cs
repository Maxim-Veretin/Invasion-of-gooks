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

        public bool IsShowExitGameMenu
        {
            get => _isShowExitGameMenu;
            private set
            {
                _isShowExitGameMenu = value;
                OnPropertyChanged();
                //OnPropertyChanged(nameof(IsPauseGame));
                PauseGame();
            }
        }
        public bool IsPauseKey
        {
            get => _isPauseKey;
            private set
            {
                _isPauseKey = value;
                OnPropertyChanged();
                //OnPropertyChanged(nameof(IsPauseGame));
                PauseGame();
            }
        }

        private void PauseGame()
        {
            if (ViewModelGameStaticProperty.SingularExemplar.IsPause = IsPauseKey || IsShowExitGameMenu || IsEndGame)
                model.GamePause();
            else
                model.GameContinue();

        }

        public bool IsEndGame
        {
            get => _isEndGame;
            private set
            {
                _isEndGame = value;
                OnPropertyChanged();
                PauseGame();
            }
            //public bool IsPauseGame => IsPauseKey || IsShowExitGameMenu;
        }
    }
}