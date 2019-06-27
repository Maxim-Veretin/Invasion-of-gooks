using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Invasion_of_Gooks.Model
{

    public class ModelClass
    {
        public Sky WarSky { get; }

        public ModelClass(/*Grid scene*/)
        {
            WarSky = new Sky(new SkySetting()
            {
                Width = 1360,
                Height = 760,
                GamerSpeed = 300,
                GamerHeight = 75,
                GamerWidth = 75,
                EnemyWidth = 75,
                EnemyHeight = 75,
                EnemySpeed = 50,
                EnemyBulletHeight = 7,
                EnemyBulletWidth = 3,
                EnemyBulletSpeed = 500,
                EnemyRocketHeight = 20,
                EnemyRocketWidth = 10,
                EnemyRocketSpeed = 500,
                GamerBulletHeight = 7,
                GamerBulletWidth = 3,
                GamerBulletSpeed = 1000,
                GamerRocketHeight = 20,
                GamerRocketWidth = 10,
                GamerRocketSpeed = 700,
                EnemyFrequency = 3,
                EnemyFrequencyProjectile = 3,
                EnemyShareRockets = 0.5,
                GamerHealth = 10,
                EnemyHealth = 3,
                EnemyBossBulletHeight = 10,
                EnemyBossBulletWidth = 6,
                EnemyBossHealth = 30,
                EnemyBossShareRockets = 0.5,
                EnemyBossFrequencyProjectile = 2,
                EnemyBossHeight = 300,
                EnemyBossWidth = 300,
                EnemyBossSpeed = 20
            }/*, scene________*/);
        }

        public bool SaveResult(DataGamer gamer)
        {
            return true;
        }

        public void SetAction(DirecionEnum direcion)
        {
            switch(direcion)
            {
                case DirecionEnum.Up:
                    WarSky.Gamer.SpeedVertical = -500;
                    WarSky.Gamer.SpeedHorizontal = 0;
                    break;

                case DirecionEnum.Right:
                    WarSky.Gamer.SpeedVertical = 0;
                    WarSky.Gamer.SpeedHorizontal = 500;
                    break;

                case DirecionEnum.Down:
                    WarSky.Gamer.SpeedVertical = 500;
                    WarSky.Gamer.SpeedHorizontal = 0;
                    break;

                case DirecionEnum.Left:
                    WarSky.Gamer.SpeedVertical = 0;
                    WarSky.Gamer.SpeedHorizontal = -500;
                    break;

                case DirecionEnum.Stop:
                    WarSky.Gamer.SpeedVertical = 0;
                    WarSky.Gamer.SpeedHorizontal = 0;
                    break;

                case DirecionEnum.UpRight:
                    WarSky.Gamer.SpeedVertical = -200;
                    WarSky.Gamer.SpeedHorizontal = 200;
                    break;

                case DirecionEnum.UpLeft:
                    WarSky.Gamer.SpeedVertical = -200;
                    WarSky.Gamer.SpeedHorizontal = -200;
                    break;

                case DirecionEnum.DownRight:
                    WarSky.Gamer.SpeedVertical = 200;
                    WarSky.Gamer.SpeedHorizontal = 200;
                    break;

                case DirecionEnum.DownLeft:
                    WarSky.Gamer.SpeedVertical = 200;
                    WarSky.Gamer.SpeedHorizontal = -200;
                    break;

                case DirecionEnum.Pif:
                    WarSky.GamerPifMethod();
                    break;

                case DirecionEnum.Paf:
                    WarSky.GamerPafMethod();
                    break;
            }
        }

        public void PauseStart(bool start)
        {
            if (start) WarSky.Continue();
            else WarSky.Pause();
        }
    }

    public enum DirecionEnum
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
        Napalm
    }
}
