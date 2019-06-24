using Invasion_of_Gooks.Model;
using Invasion_of_Gooks.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Invasion_of_Gooks.ViewModel
{
    public class ViewModelClass
    {
        public double SkyWidth { get; set; }
        public double SkyHeight { get; set; }

        private readonly ModelClass model = new ModelClass();
        public ObservableCollection<UFOClass> UFOitems { get; }
        public GamerClass Gamer { get; }

        private Sky warSky;

        public MediaPlayer gamePlayer = new MediaPlayer();
        public MediaPlayer gamePlayerPropellers = new MediaPlayer();
        public MediaPlayer pleer = new MediaPlayer();

        public ViewModelClass()
        {
            warSky = model.WarSky;

            UFOitems = warSky.UFOitems;
            Gamer = warSky.Gamer;
            SkyWidth = warSky.Width;
            SkyHeight = warSky.Heidht;

            warSky.SoundEvent += WarSky_SoundEvent;
            warSky.EndGameEvent += WarSky_EndGameEvent;

            gamePlayer.Open(new Uri("C:/Users/Администратор/Desktop/sounds/rideOfTheValkyries.wav", UriKind.Absolute));
            gamePlayer.Play();

            gamePlayerPropellers.Open(new Uri("C:/Users/Администратор/Desktop/sounds/propeller1.wav", UriKind.Absolute));
            gamePlayerPropellers.Volume = 0.1;
            gamePlayerPropellers.Play();
            
            pleer.Open(new Uri("/Invasion of Gooks;component/Resources/Sound/manyShot.wav", UriKind.Relative));
            Pleers.Add(SoundEnum.gamerShot, pleer);

            pleer = new MediaPlayer();
            pleer.Open(new Uri("/Invasion of Gooks;component/Resources/Sound/roocket.wav", UriKind.Relative));
            Pleers.Add(SoundEnum.gamerRocket, pleer);

            pleer = new MediaPlayer();
            pleer.Open(new Uri("/Invasion of Gooks;component/Resources/Sound/scream.wav", UriKind.Relative));
            Pleers.Add(SoundEnum.enemyDie, pleer);
        }

        private void WarSky_EndGameEvent(Sky sky, EndGameEnum endGame)
        {
            model.PauseStart(false);
            gamePlayer.Close();
            gamePlayerPropellers.Close();

            switch(endGame)
            {
                case EndGameEnum.Losing:
                    EndGame endgame = new EndGame();
                    endgame.ShowDialog();
                    break;

                case EndGameEnum.Win:
                    WinGame winGame = new WinGame();
                    winGame.ShowDialog();
                    break;
            }

            warSky.Pause();
        }

        private readonly Dictionary<SoundEnum, MediaPlayer> Pleers = new Dictionary<SoundEnum, MediaPlayer>();
        private void WarSky_SoundEvent(Sky sky, SoundEnum sound)
        {
            switch(sound)
            {
                case SoundEnum.enemyDie:
                    pleer.Open(new Uri("C:/Users/Администратор/Desktop/sounds/scream.wav", UriKind.Absolute));
                    pleer.Play();
                    break;

            }
            //Pleers[sound].Play();
        }

        public void KeyUser(KeyControl action)
        {
            switch (action)
            {
                case KeyControl.Pause:
                    model.PauseStart(false);
                    break;
                case KeyControl.Continue:
                    model.PauseStart(true);
                    break;

                case KeyControl.Up:
                case KeyControl.Right:
                case KeyControl.Down:
                case KeyControl.Left:
                case KeyControl.Stop:
                case KeyControl.UpRight:
                case KeyControl.UpLeft:
                case KeyControl.DownRight:
                case KeyControl.DownLeft:
                case KeyControl.Pif:
                case KeyControl Paf:
                    model.SetAction((DirecionEnum)action);
                    break;
            }
        }
    }

    /// <summary>перечисление действий</summary>
    public enum KeyControl
    {
        Up,
        Down,
        Left,
        Right,
        Stop,
        UpRight,
        UpLeft,
        DownRight,
        DownLeft,
        Pif,
        Paf,
        Napalm,
        Pause,
        Continue,
    }
}
