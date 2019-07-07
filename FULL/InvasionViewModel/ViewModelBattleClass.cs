using CommLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using InvasionModel;

namespace InvasionViewModel
{
    public partial class ViewModelBattleClass : OnPropertyChangedClass
    {
        //================================================!!!=================
        //public Window own = null;

        //public double SkyWidth { get; set; }

        public double SkyWidth { get; set; }
        public double SkyHeight { get; set; }

        //SQLiteConnection m_dbConnection;

        private readonly ModelClass model = new ModelClass();

        public ObservableCollection<UFOClass> UFOitems { get => _uFOitems; private set { _uFOitems = value; OnPropertyChanged(); } }
        public GamerClass Gamer { get => _gamer; private set { _gamer = value; OnPropertyChanged(); } }

        //public DataGamer dataGamer;

        public Sky warSky;
        public SkySetting Setting { get; }
        public string nameReceiver;
        public int scoreReceiver;
        //public MediaPlayer gamePlayer = new MediaPlayer();
        //public MediaPlayer gamePlayerPropellers = new MediaPlayer();
        public MediaPlayer pleer = new MediaPlayer();
        public MediaPlayer pleer1 = new MediaPlayer();
        public MediaPlayer pleer2 = new MediaPlayer();
        //private bool _isStop;
        private ObservableCollection<UFOClass> _uFOitems;
        private GamerClass _gamer;

        //public bool IsStop { get => _isStop; private set { _isStop = value; OnPropertyChanged(); } }
        /// <summary>Метод разрешающий выполнение команды</summary>
        /// <param name="parameter"></param>
        /// <return><see langword="true"/> если команда разрешена</returns>
        //private bool SaveResultCanMetod(object parameter)
        //{
        //    return parameter is object[] arr && arr.Length>1 && int.TryParse( arr[1].ToString(), out int _tmp);
        //}

        //private void SaveResultMetod(object parameter)
        //{
        //    if (SaveResultCanMetod(parameter))
        //    {
        //        object[] arr = (object[])parameter;
        //        model.SaveResult(new DataGamer() { Name=arr[0].ToString(), Scr = int.Parse(arr[1].ToString())});
        //    }
        //}
        //public void ResetValue()
        //{
        //    //warSky = new Sky();
        //    Gamer.Top = 760 * .9;
        //    Gamer.Left = 1360 * .5;
        //    Gamer.SpeedHorizontal = 0;
        //    Gamer.SpeedVertical = 0;
        //    //warSky.timer.Tick -= Timer_Tick;
        //    warSky.IsEndGame = false;
        //    warSky.scoreSky = 0;
        //    //warSky.haveBoss = false;
        //    warSky.Gamer.Health = 10;

        //}
        private void WarSky_ExplosionEvent(Sky sky, double top, double left, double width, double height)
        {
            throw new NotImplementedException();
        }
        private void SaveResoult()
        {
            //dataGamer.Scr = warSky.scoreSky;

            //using (SQLiteConnection connection = new SQLiteConnection(ModelDataBaseClass.DataBaseFullName))
            //{
            //    connection.Open();

            //    string sql = "INSERT INTO kyrsach (Name, Score) VALUES ('" + /*DtGamer.Name*/NameGamer + "'," + /*DtGamer.Scr*/warSky.scoreSky.ToString() + ")";

            //    SQLiteCommand command = new SQLiteCommand(sql, connection);
            //    command.ExecuteNonQuery();

            //    connection.Close();
            //}
            ModelDataBaseClass.Save(new DataGamer(ModelDataBaseClass.GamerName, warSky.scoreSky));
        }


        private void WarSky_EndGameEvent(Sky sky, EndGameEnum endGame)
        {
            //model.GamePause();
            //warSky.Pause();
            IsEndGame = true;
            IsVictory = endGame == EndGameEnum.Win;
            //gamePlayer.Close();
            //gamePlayerPropellers.Close();
            //DtGamer.Scr = warSky.scoreSky;
            //IsStop = true;

            //switch (endGame)
            //{
            //    case EndGameEnum.Losing:
            //        //SaveResoult();
            //        //EndGame endgame = new EndGame();
            //        //endgame.ShowDialog();
            //        //own.Owner.Show();
            //        //own.Close();
            //        //ResetValue();
            //        //warSky.IsEndGame = false;
            //        break;

            //    case EndGameEnum.Win:

            //        SaveResoult();
            //        //WinGame winGame = new WinGame();
            //        //winGame.ShowDialog();
            //        //================================================!!!=================
            //        //own.Owner.Show();
            //        //own.Close();
            //        //ResetValue();
            //        //warSky.IsEndGame = false;
            //        break;
            //}

        }

        //private readonly Dictionary<SoundEnum, MediaPlayer> Pleers = new Dictionary<SoundEnum, MediaPlayer>();
        private void WarSky_SoundEvent(object sender, SoundEnum sound)
        {
            OnSound(sound);
            //switch (sound)
            //{
            //    case SoundEnum.enemyDie:
            //        //на паре
            //        pleer.Open(new Uri("C:/Users/Admin/Desktop/FULL/Invasion of Gooks/Resources/Sound/bang.wav", UriKind.Absolute));

            //        //дома
            //        //pleer.Open(new Uri("C:/Users/Администратор/Desktop/Invasion of Gooks_фреймы/Invasion of Gooks/Resources/Sound/bang.wav", UriKind.Absolute));
            //        pleer.Volume = 0.5;
            //        pleer.Play();
            //        break;

            //    case SoundEnum.gamerShot:
            //        //на паре
            //        pleer1.Open(new Uri("C:/Users/Admin/Desktop/FULL/Invasion of Gooks/Resources/Sound/singleShot.wav", UriKind.Absolute));

            //        //дома
            //        //pleer1.Open(new Uri("C:/Users/Администратор/Desktop/Invasion of Gooks_фреймы/Invasion of Gooks/Resources/Sound/singleShot.wav", UriKind.Absolute));
            //        pleer1.Volume = 0.1;
            //        pleer1.Play();
            //        break;

            //    case SoundEnum.gamerRocket:
            //        //на паре
            //        pleer2.Open(new Uri("C:/Users/Admin/Desktop/FULL/Invasion of Gooks/Resources/Sound/rocket.wav", UriKind.Absolute));

            //        //дома
            //        //pleer2.Open(new Uri("C:/Users/Администратор/Desktop/Invasion of Gooks_фреймы/Invasion of Gooks/Resources/Sound/rocket.wav", UriKind.Absolute));
            //        pleer2.Volume = 0.1;
            //        pleer2.Play();
            //        break;
            //}
            //// Pleers[sound].Play();
        }

        public void KeyUser(KeyControl action)
        {
            switch (action)
            {
                case KeyControl.Pause:
                    //IsStop = true;
                    model.GamePause();
                    break;
                case KeyControl.Continue:
                    //IsStop = false;
                    model.GameContinue();
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
                case KeyControl.Paf:
                    model.SetAction((ActionEnum)action);
                    break;
            }
        }

        public RelayCommand SaveResultCommand { get; }
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
