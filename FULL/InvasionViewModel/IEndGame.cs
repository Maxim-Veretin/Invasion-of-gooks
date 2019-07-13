using CommLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvasionViewModel
{
    /// <summary>Интерфейс для контекста данных View конца игры</summary>
    public interface IEndGame : INotifyPropertyChanged
    {
        /// <summary>Игра окончена</summary>
        bool IsEndGame { get; }
        /// <summary>Игра окончена победой</summary>
        bool IsVictory { get; }

        /// <summary>Количество убитых</summary>
        int Frags { get; }
        /// <summary>Количество набранных очков</summary>
        int Score { get; }


        /// <summary>Команда старта новой игры</summary>
        RelayCommand StartGameCommand { get; }
        /// <summary>Команда выхода из игры</summary>
        RelayCommand ExitGameCommand { get; }
    }
}
