using CommLibrary;
using System;

namespace InvasionModel
{
    public class UFOClass : OnPropertyChangedClass
    {
        private double _left;
        private double _top;
        private double _speedHorizontal;
        private double _speedVertical;
        private bool _isGotShot;

        public double Width { get; }
        public double Heidht { get; }
        public double Top { get => _top; set { _top = value; OnPropertyChanged(); } }
        public double Left { get => _left; set { _left = value; OnPropertyChanged(); } }
        public double SpeedVertical { get => _speedVertical; set { _speedVertical = value; OnPropertyChanged(); } }
        public double SpeedHorizontal { get => _speedHorizontal; set { _speedHorizontal = value; OnPropertyChanged(); } }
        public bool IsGotShot { get => _isGotShot; set { _isGotShot = value; OnPropertyChanged(); } }

        public UFOClass(double width, double heidht)
        {
            Width = width;
            Heidht = heidht;
        }

        public bool Intersection(UFOClass ufo)
        {
            double a_y = Top;
            double a_y1 = Top + Heidht;
            double a_x = Left;
            double a_x1 = Left + Width;

            double b_y = ufo.Top;
            double b_y1 = ufo.Top + ufo.Heidht;
            double b_x = ufo.Left;
            double b_x1 = ufo.Left + ufo.Width;


            return (
                    (
                      (
                        (a_x >= b_x && a_x <= b_x1) || (a_x1 >= b_x && a_x1 <= b_x1)
                      ) && (
                        (a_y >= b_y && a_y <= b_y1) || (a_y1 >= b_y && a_y1 <= b_y1)
                      )
                    ) || (
                      (
                        (b_x >= a_x && b_x <= a_x1) || (b_x1 >= a_x && b_x1 <= a_x1)
                      ) && (
                        (b_y >= a_y && b_y <= a_y1) || (b_y1 >= a_y && b_y1 <= a_y1)
                      )
                    )
                  ) || (
                    (
                      (
                        (a_x >= b_x && a_x <= b_x1) || (a_x1 >= b_x && a_x1 <= b_x1)
                      ) && (
                        (b_y >= a_y && b_y <= a_y1) || (b_y1 >= a_y && b_y1 <= a_y1)
                      )
                    ) || (
                      (
                        (b_x >= a_x && b_x <= a_x1) || (b_x1 >= a_x && b_x1 <= a_x1)
                      ) && (
                        (a_y >= b_y && a_y <= b_y1) || (a_y1 >= b_y && a_y1 <= b_y1)
                      )
                    )
                  );
        }
    }

    public class ExplosionClass : UFOClass
    {
        public ExplosionClass(double width, double heidht) : base(width, heidht) { }
    }

    public class HeliopterClass : UFOClass
    {
        private DateTime _projectileTime;
        private double _health;
        private double _fullHealth;

        public HeliopterClass(double width, double heidht) : base(width, heidht) { }
        
        /// <summary>Здоровье</summary>
        public double Health { get => _health; set { _health = value; OnPropertyChanged(); } }
        /// <summary>Полное здоровье</summary>
        public double FullHealth { get => _fullHealth; set { _fullHealth = value; OnPropertyChanged(); } }
        /// <summary>Время последнего выстрела</summary>
        public DateTime ProjectileTime { get => _projectileTime; set { _projectileTime = value; OnPropertyChanged(); } }
    }

    /// <summary>Класс НЛО игрока</summary>
    public class GamerClass : HeliopterClass
    {
        public GamerClass(double width, double heidht) : base(width, heidht) { }
    }
    
    /// <summary>Класс НЛО противника</summary>
    public class EnemyClass : HeliopterClass
    {
        public EnemyClass(double width, double heidht) : base(width, heidht) { }
    }

    /// <summary>Класс босса</summary>
    public class EnemyBossClass : EnemyClass
    {
        public EnemyBossClass(double width, double heidht) : base(width, heidht) { }
    }

    /// <summary>Класс выстрелов</summary>
    public class ProjectileClass : UFOClass
    {
        public ProjectileClass(double width, double heidht) : base(width, heidht) { }
    }
     /// <summary>Класс выстрелов Игрока</summary>
    public class ProjectileGamerClass : ProjectileClass
    {
        public ProjectileGamerClass(double width, double heidht) : base(width, heidht) { }
    }
   /// <summary>Класс выстрелоу противников</summary>
    public class ProjectileEnemyClass : ProjectileClass
    {
        public ProjectileEnemyClass(double width, double heidht) : base(width, heidht) { }
    }

    /// <summary>Класс выстрел-ракетой</summary>
    public class RocketEnemyClass : ProjectileEnemyClass
    {
        private double _angle;

        public RocketEnemyClass(double width, double heidht) : base(width, heidht) { }

        public double Angle { get => _angle; set { _angle = value; OnPropertyChanged(); } }
    }

    /// <summary>Класс выстрел-снарядом</summary>
    public class BulletEnemyClass : ProjectileEnemyClass
    {
        public BulletEnemyClass(double width, double heidht) : base(width, heidht) { }
    }

    /// <summary>Класс выстрел-ракетой</summary>
    public class RocketGamerClass : ProjectileGamerClass
    {
        private double _angle;

        public RocketGamerClass(double width, double heidht) : base(width, heidht) { }

        public double Angle { get => _angle; set { _angle = value; OnPropertyChanged(); } }
    }

    /// <summary>Класс выстрел-снарядом</summary>
    public class BulletGamerClass : ProjectileGamerClass
    {
        public BulletGamerClass(double width, double heidht) : base(width, heidht) { }
    }
}
