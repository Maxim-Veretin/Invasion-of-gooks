namespace InvasionModel
{

    public class ModelClass
    {
        /// <summary>Небо игры с объектами</summary>
        public Sky WarSky { get; private set; }

        /// <summary>Задание следующего действия в игре</summary>
        /// <param name="direcion">Значение действия</param>
        public void SetAction(ActionEnum direcion)
        {
            switch (direcion)
            {
                case ActionEnum.Up:
                    WarSky.Gamer.SpeedVertical = -500;
                    WarSky.Gamer.SpeedHorizontal = 0;
                    break;

                case ActionEnum.Right:
                    WarSky.Gamer.SpeedVertical = 0;
                    WarSky.Gamer.SpeedHorizontal = 500;
                    break;

                case ActionEnum.Down:
                    WarSky.Gamer.SpeedVertical = 500;
                    WarSky.Gamer.SpeedHorizontal = 0;
                    break;

                case ActionEnum.Left:
                    WarSky.Gamer.SpeedVertical = 0;
                    WarSky.Gamer.SpeedHorizontal = -500;
                    break;

                case ActionEnum.Stop:
                    WarSky.Gamer.SpeedVertical = 0;
                    WarSky.Gamer.SpeedHorizontal = 0;
                    break;

                case ActionEnum.UpRight:
                    WarSky.Gamer.SpeedVertical = -200;
                    WarSky.Gamer.SpeedHorizontal = 200;
                    break;

                case ActionEnum.UpLeft:
                    WarSky.Gamer.SpeedVertical = -200;
                    WarSky.Gamer.SpeedHorizontal = -200;
                    break;

                case ActionEnum.DownRight:
                    WarSky.Gamer.SpeedVertical = 200;
                    WarSky.Gamer.SpeedHorizontal = 200;
                    break;

                case ActionEnum.DownLeft:
                    WarSky.Gamer.SpeedVertical = 200;
                    WarSky.Gamer.SpeedHorizontal = -200;
                    break;

                case ActionEnum.Pif:
                    WarSky.GamerPifMethod();
                    break;

                case ActionEnum.Paf:
                    WarSky.GamerPafMethod();
                    break;
            }
        }
        /// <summary>Пауза в игре</summary>
        public void GamePause()
        {
            if (WarSky != null)
                WarSky.Pause();
        }
        /// <summary>Продолжение после паузы в игре</summary>
        public void GameContinue()
        {
            if (WarSky != null)
                WarSky.Continue();
        }
        /// <summary>Выход из игры</summary>
        public void GameBreak() { WarSky = null; }
        /// <summary>Старт новой игры</summary>
        public void GameStart()
        {
            WarSky = new Sky
                (new SkySetting()
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
                }
                );
        }
    }
}
