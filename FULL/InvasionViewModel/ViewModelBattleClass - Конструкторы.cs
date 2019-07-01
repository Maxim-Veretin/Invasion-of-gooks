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
        public ViewModelBattleClass()
        {

            //StartCommand = new RelayCommand(StartMetod, StartCanMetod);

            //SaveResultCommand = new RelayCommand(SaveResultMetod,SaveResultCanMetod);

            warSky = model.WarSky;

            UFOitems = warSky.UFOitems;
            Gamer = warSky.Gamer;
            SkyWidth = warSky.Width;
            SkyHeight = warSky.Heidht;

            warSky.SoundEvent += WarSky_SoundEvent;
            warSky.EndGameEvent += WarSky_EndGameEvent;
            //warSky.ExplosionEvent += WarSky_ExplosionEvent;

            ////на паре
            ////gamePlayer.Open(new Uri("C:/Users/Admin/Desktop/Invasion of Gooks_попытка_2/Invasion of Gooks/Resources/Sound/rideOfTheValkyries.wav", UriKind.Absolute));

            ////дома
            //gamePlayer.Open(new Uri("C:/Users/Администратор/Desktop/Invasion of Gooks_фреймы/Invasion of Gooks/Resources/Sound/rideOfTheValkyries.wav", UriKind.Absolute));
            //gamePlayer.Volume = 0.4;
            //gamePlayer.Play();

            ////на паре
            ////gamePlayerPropellers.Open(new Uri("C:/Users/Admin/Desktop/Invasion of Gooks_попытка_2/Invasion of Gooks/Resources/Sound/propeller1.wav", UriKind.Absolute));

            ////дома
            //gamePlayerPropellers.Open(new Uri("C:/User/Администратор/Desktop/Invasion of Gooks_фреймы/Invasion of Gooks/Resources/Sound/propeller1.wav", UriKind.Absolute));
            //gamePlayerPropellers.Volume = 0.3;
            //gamePlayerPropellers.Play();
        }

    }
}
