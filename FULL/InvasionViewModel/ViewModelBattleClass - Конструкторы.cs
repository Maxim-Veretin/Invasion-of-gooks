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
        /// <summary>Сокрытие безпараметрического конструтора</summary>
        private ViewModelBattleClass()
        {


        }
        /// <summary>Конструтор с методами для команды выхода из игры</summary>
        /// <param name="exitGameMetod">Исполняющий метод команды</param>
        /// <param name="exitGameCanMetod">Проыеряющий метод команды</param>
        public ViewModelBattleClass(ExecuteHandler exitGameMetod, CanExecuteHandler exitGameCanMetod) : this()
        {
            ExitGameCommand = new RelayCommand(exitGameMetod, exitGameCanMetod);
        }
    }
}
