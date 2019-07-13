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

        /// <summary>Команда для старта игры</summary>
        public RelayCommand StartCommand { get; }
        /// <summary>Команда для выхода</summary>
        public RelayCommand ExitCommand { get; }

        /// <summary>Конструктор с методами для команд</summary>
        /// <param name="executeStartCommand">Исполняющий метод команды старта игры</param>
        /// <param name="canExecuteStartCommand">Проверяющий метод команды старта игры</param>
        /// <param name="executeExitCommand">Исполняющий метод команды выхода</param>
        /// <param name="canExecuteExitCommand">Проверяющий метод команды выхода</param>
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
