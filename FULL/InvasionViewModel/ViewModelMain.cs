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
        private object content;

        public ViewModelBattleClass ViewModelBattle { get; } = new ViewModelBattleClass();
        public ViewModelDataBaseClass ViewModelDataBase { get; }

        public object Content { get => content; set { content = value; OnPropertyChanged(); } }

        public ViewModelMain(Action exitApplication)
        {
            content = ViewModelDataBase;
            ViewModelDataBase = new ViewModelDataBaseClass
                (
                    StartMetod,
                    (p) => true,
                    ExitMetod,
                    (p) => true
                );
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
