using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;

namespace Invasion_of_Gooks
{
    public enum MediaPlayerEnum
    {
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
        public static readonly IDictionary<MediaPlayerEnum, MediaPlayer> Players = new Dictionary<MediaPlayerEnum, MediaPlayer>();
        public static IDictionary<MediaPlayerEnum, string> NameFiles = new Dictionary<MediaPlayerEnum, string>()
        {
            { MediaPlayerEnum.bang, "bang.wav"},
            { MediaPlayerEnum.MainWindow, "menuSingle.wav"},
        };

        /// <summary>Статический конструтор</summary>
        static MediaPlayerExtensions()
        {
            string folder = Directory.GetCurrentDirectory() + "\\Resources\\Sound\\";
            foreach (KeyValuePair<MediaPlayerEnum, string> item in NameFiles)
            {
                var qwer = new Uri(folder + item.Value, UriKind.Relative);
                Players.Add(item.Key, new MediaPlayer());
                Players[item.Key].Open(new Uri(folder + item.Value));
            }
        }
        /// <summary>Проирывание связанного медиа</summary>
        /// <param name="sound">Значение медиа</param>
        /// <param name="loop"><see langword="true"/> для циклическогго проигрывания</param>
        public static void Play(this MediaPlayerEnum sound, bool loop = false)
        {
            Players[sound].Position = TimeSpan.Zero;
            Players[sound].Play();
            Players[sound].MediaEnded -= MediaPlayerExtensions_MediaEnded;
            if (loop)
                Players[sound].MediaEnded += MediaPlayerExtensions_MediaEnded;
        }
        /// <summary>Остановка (пауза) связанного медиа</summary>
        /// <param name="sound">Значение медиа</param>
        public static void Pause(this MediaPlayerEnum sound)
        {
            Players[sound].Pause();
        }
        /// <summary>Прдолжение воспроизведения связанного медиа</summary>
        /// <param name="sound">Значение медиа</param>
        public static void Continue(this MediaPlayerEnum sound)
        {
            Players[sound].Play();
        }

        /// <summary>Повтор проигрывания</summary>
        /// <param name="sender">MediaPlayer</param>
        /// <param name="e">Не используется</param>
        private static void MediaPlayerExtensions_MediaEnded(object sender, EventArgs e)
        {
            MediaPlayer player = (MediaPlayer)sender;
            player.Position = TimeSpan.Zero;
            player.Play();
        }
    }
}
