using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CommLibrary
{
    public static class CommandExtensions
    {
        /// <summary>Выполнение WPF команды</summary>
        /// <param name="command">Команда</param>
        /// <param name="parameter">Параметр</param>
        public static void CommandExecute(this ICommand command, object parameter)
        {
            if (command.CanExecute(parameter))
                command.Execute(parameter);
        }

    }
}
