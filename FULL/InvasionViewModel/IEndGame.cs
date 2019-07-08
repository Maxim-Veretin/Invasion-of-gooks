using CommLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvasionViewModel
{
    public interface IEndGame : INotifyPropertyChanged
    {
        bool IsEndGame { get; }
        bool IsVictory { get; }

        int Frags { get; }
        int Score { get; }


        RelayCommand StartGameCommand { get; }
        RelayCommand ExitGameCommand { get; }
    }
}
