using CommLibrary;
using InvasionModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvasionViewModel
{
    public class ViewModelDataBaseClass : OnPropertyChangedClass
    {
        /// <summary> Свойство, возвращает список из поля </summary>
        public ObservableCollection<DataGamer> DataGamers => ModelDataBaseClass.DataGamers;
        /// <summary>Имя файла и формат</summary>
        public string FileName { get; } = ModelDataBaseClass.FileName;
        ///// <summary>Имя игрока</summary>
        //public string GamerName
        //{
        //    get => ModelDataBaseClass.GamerName;
        //    set
        //    {
        //        ModelDataBaseClass.GamerName = value;
        //        OnPropertyChanged();
        //        StartCommand.Invalidate();
        //    }
        //}
        public RelayCommand StartCommand { get; }
        public RelayCommand ExitCommand { get; }

        public ViewModelDataBaseClass
            (
                ExecuteHandler executeStartCommand, CanExecuteHandler canExecuteStartCommand,
                ExecuteHandler executeExitCommand, CanExecuteHandler canExecuteExitCommand
            )
        {
            StartCommand = new RelayCommand
                (
                    (p) => { ModelDataBaseClass.GamerName = p as string; executeStartCommand(p); },
                    (p) => p is string pStr && !string.IsNullOrWhiteSpace(pStr) && canExecuteStartCommand(p)
                );
            ExitCommand = new RelayCommand(executeExitCommand, (p) => canExecuteExitCommand(p));
        }

    }
}
