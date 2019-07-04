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
        //public static ViewModelClass ViewModel { get; } = new ViewModelClass();
        private ViewModelBattleClass()
        {

            //StartCommand = new RelayCommand(StartMetod, StartCanMetod);

            //SaveResultCommand = new RelayCommand(SaveResultMetod,SaveResultCanMetod);

            //warSky = model.WarSky;

            //UFOitems = warSky.UFOitems;
            //Gamer = warSky.Gamer;
            //SkyWidth = warSky.Width;
            //SkyHeight = warSky.Heidht;

            //warSky.SoundEvent += WarSky_SoundEvent;
            //warSky.EndGameEvent += WarSky_EndGameEvent;
            //warSky.ExplosionEvent += WarSky_ExplosionEvent;

        }

        public ViewModelBattleClass(ExecuteHandler exitGameMetod, CanExecuteHandler exitGameCanMetod) : this()
        {
            ExitGameCommand = new RelayCommand(exitGameMetod, exitGameCanMetod);
        }
    }
}
