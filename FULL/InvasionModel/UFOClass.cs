using CommLibrary;
using System;

namespace InvasionModel
{
    public class UFOClass : OnPropertyChangedClass, ICopy<UFOClass>
    {
        private double _left;
        private double _top;
        private double _speedHorizontal;
        private double _speedVertical;
        //private bool _isGotShot;
        private double _width;
        private double _heidht;

        /// <summary>Ширина</summary>
        public double Width { get => _width; set { _width = value; OnPropertyChanged(); } }
        /// <summary>Высота</summary>
        public double Heidht { get => _heidht; set { _heidht = value; OnPropertyChanged(); } }
        /// <summary>Смещение сверху</summary>
        public double Top { get => _top; set { _top = value; OnPropertyChanged(); } }
        /// <summary>Смещение слева</summary>
        public double Left { get => _left; set { _left = value; OnPropertyChanged(); } }
        /// <summary>Вертикальная скорость</summary>
        public double SpeedVertical { get => _speedVertical; set { _speedVertical = value; OnPropertyChanged(); } }
        /// <summary>Горизонтальная скорость</summary>
        public double SpeedHorizontal { get => _speedHorizontal; set { _speedHorizontal = value; OnPropertyChanged(); } }
        //public bool IsGotShot { get => _isGotShot; set { _isGotShot = value; OnPropertyChanged(); } }

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

        public void CopyTo(UFOClass other)
        {
            other.Width = Width;
            other.Heidht = Heidht;
            //IsGotShot = IsGotShot;
            other.Left = Left;
            other.SpeedHorizontal = SpeedHorizontal;
            other.SpeedVertical = SpeedVertical;
            other.Top = Top;
        }

        public void CopyFrom(UFOClass other)
        {
            Width = other.Width;
            Heidht = other.Heidht;
            //IsGotShot = IsGotShot;
            Left = other.Left;
            SpeedHorizontal = other.SpeedHorizontal;
            SpeedVertical = other.SpeedVertical;
            Top = other.Top;
        }

        public object Clone() => Copy<UFOClass>();

        public virtual T1 Copy<T1>() where T1 : UFOClass, new()
        {
            return new T1()
            {
                Width = Width,
                Heidht = Heidht,
                //IsGotShot = IsGotShot,
                Left = Left,
                SpeedHorizontal = SpeedHorizontal,
                SpeedVertical = SpeedVertical,
                Top = Top
            };
        }
    }

    public class ExplosionClass : UFOClass
    {
        private bool _isRemove = true;
        /// <summary>Элементы с установленным свойством должны быть удалены</summary>
        public bool IsRemove { get => _isRemove; private set { _isRemove = value; OnPropertyChanged(); } }
        public void Remove() => IsRemove = true;
    }

    public class HeliopterClass : UFOClass
    {
        private DateTime _projectileTime;
        private double _health;
        private double _fullHealth;

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
    }

    /// <summary>Класс НЛО противника</summary>
    public class EnemyClass : HeliopterClass
    {
    }

    /// <summary>Класс босса</summary>
    public class EnemyBossClass : EnemyClass
    {
    }

    /// <summary>Класс выстрелов</summary>
    public class ProjectileClass : UFOClass
    {
    }
    /// <summary>Класс выстрелов Игрока</summary>
    public class ProjectileGamerClass : ProjectileClass
    {
    }
    /// <summary>Класс выстрелов противников</summary>
    public class ProjectileEnemyClass : ProjectileClass
    {
    }

    /// <summary>Класс выстрел-ракетой</summary>
    public class RocketEnemyClass : ProjectileEnemyClass
    {
        private double _angle;

        public double Angle { get => _angle; set { _angle = value; OnPropertyChanged(); } }
    }

    /// <summary>Класс выстрел-снарядом</summary>
    public class BulletEnemyClass : ProjectileEnemyClass
    {
    }

    /// <summary>Класс выстрел-ракетой</summary>
    public class RocketGamerClass : ProjectileGamerClass
    {
        private double _angle;

        public double Angle { get => _angle; set { _angle = value; OnPropertyChanged(); } }
    }

    /// <summary>Класс выстрел-снарядом</summary>
    public class BulletGamerClass : ProjectileGamerClass
    {
    }
}
