using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media;

namespace Invasion_of_Gooks
{
    /// <summary>Перечиление значений используемых плееров</summary>
    public enum MediaPlayerEnum
    {
        /// <summary>Взрыв</summary>
        Bang,
        bangBoom,
        bossDie,
        Death,
        lowHPBoss,
        manyShot,
        menuSingle,
        propeller,
        /// <summary>Звук винтов вертолётов</summary>
        Propeller1,
        /// <summary>Фоновая музыка в игре</summary>
        RideOfTheValkyries,
        /// <summary>Пуск ракеты</summary>
        Rocket,
        scream,
        /// <summary>Выстрел </summary>
        SingleShot,
        spawnBoss,
        Смешарики___От_винта_bassboosted,
        /// <summary>Для стартовой страницы</summary>
        MainWindow
    }
    /// <summary>Класс расширений для плееров</summary>
    public static class MediaPlayerExtensions
    {
        /// <summary>Класс с дополинтеоьными свойствами для управления плеером</summary>
        public class PlayersStruct
        {
            /// <summary>Плеер</summary>
            public MediaPlayer Player { get; }
            /// <summary>Плеер запущен</summary>
            public bool Play { get; set; }
            /// <summary>Плеер на паузе</summary>
            public bool Pause { get; set; }
            /// <summary>Имя источника для плеера</summary>
            public string UriString { get; }
            /// <summary>Uri источника для плеера</summary>
            public Uri Uri { get; }
            /// <summary>Плеер на повторе</summary>
            public bool Repeat { get; set; }
            /// <summary>Конструктор плеера с заданием источника</summary>
            /// <param name="uriString">Имя источника для плеера</param>
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
                Pause = false;
                Repeat = false;
                Player.MediaEnded += MediaPlayerExtensions_MediaEnded;
            }

        }

        /// <summary>Словарь сопоставление значений плееров с источниками</summary>
        public static readonly IDictionary<MediaPlayerEnum, PlayersStruct> Players = new (MediaPlayerEnum player, string uri)[]
        {
           (MediaPlayerEnum.MainWindow, "menuSingle.wav"),
           (MediaPlayerEnum.Bang, "bang.wav"),
           (MediaPlayerEnum.SingleShot, "singleShot.wav"),
           (MediaPlayerEnum.RideOfTheValkyries, "rideOfTheValkyries.wav"),
           (MediaPlayerEnum.Propeller1, "propeller1.wav"),
           (MediaPlayerEnum.Rocket, "rocket.wav"),

        }
        .ToDictionary(item => item.player, item => new PlayersStruct(item.uri));

        /// <summary>Проирывание связанного медиа</summary>
        /// <param name="sound">Значение медиа</param>
        public static void Play(this MediaPlayerEnum sound) => sound.Play(false);

        /// <summary>Проирывание связанного медиа</summary>
        /// <param name="sound">Значение медиа</param>
        /// <param name="repeat"><see langword="true"/> для циклического проигрывания</param>
        public static void Play(this MediaPlayerEnum sound, bool repeat)
        {
            Players[sound].Repeat = repeat;
            Players[sound].Player.Position = TimeSpan.Zero;
            Players[sound].Pause = false;
            Players[sound].Play = true;
            if (!IsAllPause)
                Players[sound].Player.Play();
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
            if (Players[sound].Play)
            {
                Players[sound].Pause = true;
                Players[sound].Player.Pause();
            }
        }

        /// <summary>Продолжение воспроизведения связанного медиа</summary>
        /// <param name="sound">Значение медиа</param>
        public static void Continue(this MediaPlayerEnum sound)
        {
            if (Players[sound].Play && Players[sound].Pause)
            {
                Players[sound].Pause = false;
                if (!IsAllPause)
                    Players[sound].Player.Play();
            }
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

        /// <summary>Состояние паузы для всех плееров</summary>
        public static bool IsAllPause { get; private set; }

        /// <summary>Пауза всем плеерам</summary>
        public static void AllPause()
        {
            IsAllPause = true;
            foreach (var item in Players)
            {
                item.Value.Player.Pause();
            }
        }

        /// <summary>Продолжить всем плеерам</summary>
        public static void AllContinue()
        {
            IsAllPause = false;
            foreach (var item in Players)
            {
                if (item.Value.Play && !item.Value.Pause)
                {
                    item.Value.Player.Play();
                }
            }
        }
    }
}
