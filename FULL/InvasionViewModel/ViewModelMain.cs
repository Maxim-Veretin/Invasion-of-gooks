using CommLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvasionViewModel
{
    public class ViewModelMain : OnPropertyChangedClass
    {
        private object _content;

        public ViewModelBattleClass ViewModelBattle { get; }
        public ViewModelDataBaseClass ViewModelDataBase { get; }

        public object Content { get => _content; set { _content = value; OnPropertyChanged(); } }

        public ViewModelMain(Action exitApplication)
        {
            _content = ViewModelDataBase;
            ViewModelDataBase = new ViewModelDataBaseClass
                (
                    StartMetod,
                    (p) => true,
                    ExitMetod,
                    (p) => true
                );
            ViewModelBattle = new ViewModelBattleClass
                (
                    MainMetod,
                    (p) => true
                );
        }

        private void MainMetod(object parameter)
        {
            Content=ViewModelDataBase;
        }

        private void ExitMetod(object parameter)
        {
            throw new NotImplementedException();
        }

        private void StartMetod(object parameter)
        {
            Content = ViewModelBattle;
        }
    }
}
