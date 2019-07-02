using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media;

namespace Invasion_of_Gooks
{
    public enum MediaPlayerEnum
    {
        /// <summary>Смерть противника</summary>
        enemyDie,
        /// <summary>Выстрел игрока</summary>
        gamerShot,
        /// <summary>Пуск ракеты</summary>
        gamerRocket,
        bang,
        bangBoom,
        bossDie,
        Death,
        lowHPBoss,
        manyShot,
        menuSingle,
        propeller,
        propeller1,
        rideOfTheValkyries,
        rocket,
        scream,
        singleShot,
        spawnBoss,
        Смешарики___От_винта_bassboosted,
        MainWindow
    }

    public static class MediaPlayerExtensions
    {
        public class PlayersStruct
        {
            public MediaPlayer Player { get; }
            public bool Play { get; set; }
            public string UriString { get; }
            public Uri Uri { get; }
            public bool Repeat { get; set; }
            public PlayersStruct(string uriString)
            {
                UriString = uriString;
                if (Uri.TryCreate(UriString, UriKind.Relative, out Uri uri) && File.Exists(UriString))
                    Uri = uri;
                else if (Uri.TryCreate(UriString, UriKind.Absolute, out uri) && File.Exists(uri.AbsolutePath))
                    Uri = uri;
                else
                    Uri = new Uri(Directory.GetCurrentDirectory() + "\\Resources\\Sound\\" + UriString, UriKind.RelativeOrAbsolute);
                Player = new MediaPlayer();
                Player.Open(Uri);
                Play = false;
                Repeat = false;
                Player.MediaEnded += MediaPlayerExtensions_MediaEnded;
            }

        }
        public static KeyValuePair<MediaPlayerEnum, PlayersStruct> ToDictPlayer(this MediaPlayerEnum sound, string uriString)
            => new KeyValuePair<MediaPlayerEnum, PlayersStruct>(sound, new PlayersStruct(uriString));
        public static readonly IDictionary<MediaPlayerEnum, PlayersStruct> Players = new (MediaPlayerEnum player, string uri)[]
        {
           (MediaPlayerEnum.MainWindow, "menuSingle.wav"),
           (MediaPlayerEnum.enemyDie, "bang.wav"),
           (MediaPlayerEnum.gamerShot, "singleShot.wav"),
           (MediaPlayerEnum.gamerRocket, "rocket.wav"),
        }
        .ToDictionary(item => item.player, item => new PlayersStruct(item.uri));
        //public static IDictionary<MediaPlayerEnum, bool> PlayersPlay = new Dictionary<MediaPlayerEnum, bool>();

        ///// <summary>Статический конструтор</summary>
        //static MediaPlayerExtensions()
        //{
        //    string folder = Directory.GetCurrentDirectory() + "\\Resources\\Sound\\";
        //    foreach (KeyValuePair<MediaPlayerEnum, string> item in NameFiles)
        //    {
        //        var qwer = new Uri(folder + item.Value, UriKind.Relative);
        //        Players[item.Key] = new MediaPlayer();
        //        Players[item.Key].Open(new Uri(folder + item.Value));
        //    }
        //}
        /// <summary>Проирывание связанного медиа</summary>
        /// <param name="sound">Значение медиа</param>
        /// <param name="repeat"><see langword="true"/> для циклическогго проигрывания</param>
        public static void Play(this MediaPlayerEnum sound)
        {
            Players[sound].Player.Position = TimeSpan.Zero;
            Players[sound].Player.Play();
            Players[sound].Play = true;
        }
        public static void Play(this MediaPlayerEnum sound, bool repeat)
        {
            sound.Play();
            Players[sound].Repeat = repeat;
        }
        /// <summary>Остановка связанного медиа</summary>
        /// <param name="sound">Значение медиа</param>
        public static void Stop(this MediaPlayerEnum sound)
        {
            Players[sound].Player.Position = TimeSpan.Zero;
            Players[sound].Player.Stop();
            Players[sound].Play = false;
        }
        /// <summary>Остановка (пауза) связанного медиа</summary>
        /// <param name="sound">Значение медиа</param>
        public static void Pause(this MediaPlayerEnum sound)
        {
            Players[sound].Player.Pause();
        }
        /// <summary>Прдолжение воспроизведения связанного медиа</summary>
        /// <param name="sound">Значение медиа</param>
        public static void Continue(this MediaPlayerEnum sound)
        {
            if (Players[sound].Play)
                Players[sound].Player.Play();
        }


        /// <summary>Повтор проигрывания</summary>
        /// <param name="sender">MediaPlayer</param>
        /// <param name="e">Не используется</param>
        private static void MediaPlayerExtensions_MediaEnded(object sender, EventArgs e)
        {
            KeyValuePair<MediaPlayerEnum, PlayersStruct> player = Players.FirstOrDefault(item => item.Value.Player == sender);

            if (player.Value.Repeat)
                player.Key.Play();
            else
                player.Value.Play = false;
        }

        public static void AllPause()
        {
            foreach (var item in Players)
            {
                item.Key.Pause();
            }
        }

        public static void AllContinue()
        {
            foreach (var item in Players)
            {
                item.Key.Continue();
            }
        }
    }
}
