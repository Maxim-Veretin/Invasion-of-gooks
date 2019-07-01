namespace InvasionModel
{
    public struct SkySetting
    {
        /// <summary>Ширина неба</summary>
        public double Width { get; set; }
        /// <summary>Высота неба</summary>
        public double Height { get; set; }
        /// <summary>Скорость игрока</summary>
        public double GamerSpeed { get; set; }
        /// <summary>Ширина игрока</summary>
        public double GamerWidth { get; set; }
        /// <summary>Высота игрока</summary>
        public double GamerHeight { get; set; }
        /// <summary>Скорость противника</summary>
        public double EnemySpeed { get; set; }
        /// <summary>Ширина противника</summary>
        public double EnemyWidth { get; set; }
        /// <summary>Высота противника</summary>
        public double EnemyHeight { get; set; }
        /// <summary>Скорость снарядов игрока</summary>
        public double GamerBulletSpeed { get; set; }
        /// <summary>Ширина снарядов игрока</summary>
        public double GamerBulletWidth { get; set; }
        /// <summary>Высота снарядов игрока</summary>
        public double GamerBulletHeight { get; set; }
        /// <summary>Скорость ракет игрока</summary>
        public double GamerRocketSpeed { get; set; }
        /// <summary>Ширина ракет игрока</summary>
        public double GamerRocketWidth { get; set; }
        /// <summary>Высота ракет игрока</summary>
        public double GamerRocketHeight { get; set; }
        /// <summary>Скорость снарядов противника</summary>
        public double EnemyBulletSpeed { get; set; }
        /// <summary>Ширина снарядов противника</summary>
        public double EnemyBulletWidth { get; set; }
        /// <summary>Высота снарядов противника</summary>
        public double EnemyBulletHeight { get; set; }
        /// <summary>Скорость ракет противника</summary>
        public double EnemyRocketSpeed { get; set; }
        /// <summary>Ширина ракет противника</summary>
        public double EnemyRocketWidth { get; set; }
        /// <summary>Высота ракет противника</summary>
        public double EnemyRocketHeight { get; set; }
        /// <summary>Частота появления противников в секyндах</summary>
        public double EnemyFrequency { get; set; }
        /// <summary>Частота появления снарядов в секyндах у противника</summary>
        public double EnemyFrequencyProjectile { get; set; }
        /// <summary>Доля ракет в выстрелах противника</summary>
        public double EnemyShareRockets { get; set; }
        /// <summary>Здоровье игрока</summary>
        public double GamerHealth { get; set; }
        /// <summary>Здоровье рядовых противников</summary>
        public double EnemyHealth { get; set; }
        /// <summary>Частота появления снарядов в секyндах у босса</summary>
        public double EnemyBossFrequencyProjectile { get; set; }
        /// <summary>Доля ракет в выстрелах босса</summary>
        public double EnemyBossShareRockets { get; set; }
        /// <summary>Здоровье босса</summary>
        public double EnemyBossHealth { get; set; }
        /// <summary>Ширина снарядов босса</summary>
        public double EnemyBossBulletWidth { get; set; }
        /// <summary>Высота снарядов босса</summary>
        public double EnemyBossBulletHeight { get; set; }
        /// <summary>Ширина босса</summary>
        public double EnemyBossWidth { get; set; }
        /// <summary>Высота босса</summary>
        public double EnemyBossHeight { get; set; }
        /// <summary>Скорость босса</summary>
        public double EnemyBossSpeed { get; set; }
    }
}
