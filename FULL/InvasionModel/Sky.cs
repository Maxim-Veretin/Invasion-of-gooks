using CommLibrary;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;

namespace InvasionModel
{

    public delegate void SoundHandler(object sender, SoundEnum sound);
    public delegate void EndGameHandler(Sky sky, EndGameEnum endGame);
    public delegate void ExplosionHandler(Sky sky, double top, double left, double width, double height);

    /// <summary>Класс неба</summary>
    public class Sky : OnPropertyChangedClass
    {

        private int _frags = 0;
        /// <summary>количество yбитых врагов</summary>
        public int Frags
        {
            get => _frags; private set
            {
                /// Если количество убитых превысило 10,
                /// то создаётся БОСС
                if (_frags < 10 && value > 9)
                    CreateBoss();
                _frags = value;
                OnPropertyChanged();
            }
        }

        private int _score;
        /// <summary>Набранные очки</summary>
        public int Score { get => _score; set { _score = value; OnPropertyChanged(); } }
        //public bool bossDie = false;

        /// <summary>Параметры неба</summary>
        public SkySetting Setting { get; }

        /// <summary>Событие вызова звука</summary>
        public event SoundHandler SoundEvent;

        /// <summary>Вспомогательный метод для вызова события звука</summary>
        /// <param name="sound"></param>
        private void OnSound(SoundEnum sound) => SoundEvent?.Invoke(this, sound);

        /// <summary>Событие конец игры</summary>
        public event EndGameHandler EndGameEvent;

        /// <summary>Вспомогательный метод для вызова события конец игры</summary>
        private void OnEndGame(EndGameEnum endGame)
        {
            IsEndGame = true;
            EndGameEvent?.Invoke(this, endGame);
        }

        /// <summary>Ширина неба</summary>
        public double Width { get; }
        /// <summary>Высота неба</summary>
        public double Heidht { get; }
        /// <summary>Пропорции неба</summary>
        public double Ratio { get; }

        ///// <summary>Игра закончена</summary>
        public bool IsEndGame { get; private set; }

        /// <summary>Объект игрока</summary>
        public GamerClass Gamer { get; }

        /// <summary>Частота появления противников в секyндах</summary>
        public double Frequency { get; private set; }

        /// <summary>Частота появления снарядов в секyндах</summary>
        public double FrequencyProjectile { get; private set; }
        /// <summary>Доля ракет в выстрелах</summary>
        public double ShareRockets { get; private set; }

        /// <summary>все объекты в небе</summary>
        public ObservableCollection<UFOClass> UFOitems { get; } = new ObservableCollection<UFOClass>();

        public DispatcherTimer timer = new DispatcherTimer();
        /// <summary>Время последнего кадра</summary>
        private DateTime timeOld;

        public Sky(SkySetting setting)
        {
            Setting = setting;
            Width = Setting.Width;
            Heidht = Setting.Height;
            Ratio = Width / Heidht;

            //создание игрока
            Gamer = new GamerClass()
            {
                Width = Setting.GamerWidth,
                Height = Setting.GamerHeight,
                SpeedVertical = 0,
                SpeedHorizontal = 0,
                Left = Setting.Width * .5,
                Top = Setting.Height * .9,
                Health = Setting.GamerHealth,
                FullHealth = Setting.GamerHealth
            };
            UFOitems.Add(Gamer);

            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += Timer_Tick;
            timeOld = DateTime.Now;

            Frequency = Setting.EnemyFrequency;
            FrequencyProjectile = Setting.EnemyFrequencyProjectile;
            ShareRockets = Setting.EnemyShareRockets;

            oldEnemy = timeOld;
        }

        /// <summary>Время создания предыдyщего врага</summary>
        private DateTime oldEnemy;

        private static readonly Random random = new Random();

        public void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            if (IsEndGame)
                return;

            //делаем так, потомy что больше timeOld не нyжна 
            DateTime timeNew = DateTime.Now;
            TimeSpan intervalFrame = timeNew - timeOld;
            timeOld = timeNew;
            FrameMetod(intervalFrame.TotalMilliseconds * 0.001);
            if (IsEndGame)
                return;
            timer.Start();
        }

        /// <summary>Метод создания модели кадра</summary>
        /// <param name="intervalSec">Время прошедшее с прошлого кадра</param>
        private void FrameMetod(double intervalSec)
        {
            MoveUFO(intervalSec);
            ProjectiliesEnemies();
            HittingGamer();
            HittingEnemies();
            CreateEnemies();
            RemoveDead();
        }

        /// <summary>Удаление погибших</summary>
        private void RemoveDead()
        {
            foreach (UFOClass ufo in UFOitems.Where(uf => uf.Health < 0).ToArray())
                UFOitems.Remove(ufo);
        }

        /// <summary>Расчёт попаданий по игроку</summary>
        public void HittingGamer()
        {
            /// Список типов кто может нанести удар по игроку
            Type[] typeHitting = { typeof(EnemyClass), typeof(BulletEnemyClass), typeof(RocketEnemyClass) };

            /// Массив элементов из UFOitems наносящих удар по игроку
            UFOClass[] hittUFO = UFOitems
                .Where(it => typeHitting.Contains(it.GetType()))
                .Where(it => Gamer.Intersection(it))
                .ToArray();

            /// Цикл по масиву
            foreach (UFOClass hUFO in hittUFO)
            {

                /// Столкновение с противником
                if (hUFO is HeliopterClass enemy)
                {
                    UFOitems.Remove(enemy);
                    Gamer.Health -= 5;
                }
                /// Столкновение с ракетой
                else if (hUFO is RocketEnemyClass rocket)
                {
                    UFOitems.Remove(rocket);
                    Gamer.Health -= 3;
                }
                /// Столкновение со снарядом
                else if (hUFO is BulletEnemyClass bullet)
                {
                    UFOitems.Remove(bullet);
                    Gamer.Health -= 1;
                }

                /// Проверка оставшейся жизни игрока
                if (Gamer.Health <= 0)
                {
                    timer.Stop();


                    OnEndGame(EndGameEnum.Losing);

                    return;
                }
            }
        }

        /// <summary>Расчёт попаданий по противнику</summary>
        public void HittingEnemies()
        {

            /// Массив из UFOitems выстрелов игрока
            UFOClass[] hittUFO = UFOitems
                .OfType<ProjectileGamerClass>()
                .ToArray();

            /// Массив из UFOitems противников игрока
            UFOClass[] enemies = UFOitems
                .OfType<EnemyClass>()
                .ToArray();

            /// Цикл по всем противникам
            foreach (EnemyClass enemy in enemies)
            {
                /// Цикл по всем выстрелам попавшим в данного противника
                foreach (UFOClass hUFO in hittUFO.Where(it => it.Intersection(enemy)))
                {
                    /// Попадание ракетой
                    if (hUFO is RocketGamerClass rocket)
                    {
                        UFOitems.Remove(rocket);
                        enemy.Health -= 3;
                    }
                    /// Попадание снарядом
                    else if (hUFO is BulletGamerClass bullet)
                    {
                        UFOitems.Remove(bullet);
                        enemy.Health -= 1;
                    }
                    /// Проверка оставшейся у противника жизни 
                    if (enemy.Health <= 0)
                    {
                        /// Декремент количества убитых
                        Frags++;
                        /// Вызов звукового события
                        OnSound(SoundEnum.enemyDie);

                        /// Если противник был БОССом
                        if (enemy is EnemyBossClass)
                        {
                            Score += 200;
                            OnEndGame(EndGameEnum.Win);
                        }
                        else
                        {
                            Score += 20;
                        }

                        /// Удаление убитого противника
                        UFOitems.Remove(enemy);

                        /// Создание взрыва на месте убитого противника
                        ExplosionClass explosion = enemy.Copy<ExplosionClass>();
                        explosion.SpeedHorizontal = 0;
                        explosion.SpeedVertical = 0;
                        UFOitems.Add(explosion);

                        return;
                    }
                }
            }
        }



        /// <summary>Метод стрельбы противника</summary>
        private void ProjectiliesEnemies()
        {
            foreach (EnemyClass enemy
                in UFOitems // Список всех UFO
                .OfType<EnemyClass>() // Выбираем всех противников по типу класса
                .Where(_en => _en.ProjectileTime.AddSeconds(Setting.EnemyFrequencyProjectile) < DateTime.Now) // Выбираем всех у которых подошло время на следующий выстрел
                .ToArray())// Полученный список преобразуем в массив
            {
                ProjectileEnemyClass projectile; // Создаваемый выстрел

                if (random.NextDouble() > Setting.EnemyShareRockets)
                {
                    // Выстрел снарядом
                    projectile = new BulletEnemyClass()
                    {
                        Width = Setting.EnemyBulletWidth,
                        Height = Setting.EnemyBulletHeight,
                        Left = enemy.Left + enemy.Width * .5,
                        Top = enemy.Top + enemy.Height,
                        SpeedVertical = Setting.EnemyBulletSpeed
                    };
                }
                else
                {
                    // Выстрел ракетой
                    double rTop = enemy.Top + enemy.Height;
                    double rLeft = enemy.Left + enemy.Width * .5;
                    double dTop = (Gamer.Top + Gamer.Height * .5) - rTop;
                    double dLeft = (Gamer.Left + Gamer.Width * .5) - rLeft;
                    double dLenght = Math.Sqrt(dTop * dTop + dLeft * dLeft);
                    double rAngle = dLeft > dTop ? Math.Atan(dTop / dLeft) : Math.PI * .5 - Math.Atan(dLeft / dTop);
                    rAngle = 180 * rAngle / Math.PI;
                    if (dLeft <= 0)
                    {
                        if (dTop >= 0)

                            rAngle += 180;
                        else
                            rAngle -= 180;
                    }
                    double rSpeed = Setting.EnemyRocketSpeed;
                    double rSpeedV = (rSpeed * dTop) / dLenght;
                    double rSpeedH = (rSpeed * dLeft) / dLenght;
                    projectile = new RocketEnemyClass()
                    {
                        Width = Setting.EnemyRocketWidth,
                        Height = Setting.EnemyRocketHeight,
                        Left = rLeft,
                        Top = rTop,
                        SpeedVertical = rSpeedV,
                        SpeedHorizontal = rSpeedH,
                        Angle = rAngle - 90
                    };
                }
                UFOitems.Add(projectile);
                enemy.ProjectileTime = DateTime.Now;
            }
        }

        /// <summary>Метод создания противников</summary>
        private void CreateEnemies()
        {
            //yбрать oldEnemy = timeOld для орды
            if ((timeOld - oldEnemy).TotalSeconds > Setting.EnemyFrequency)
            {
                EnemyClass enemy = new EnemyClass()
                {
                    Width = Setting.EnemyWidth,
                    Height = Setting.EnemyHeight,
                    Top = 0,
                    Left = random.Next((int)(Width - Gamer.Width)),
                    SpeedVertical = Setting.EnemySpeed,
                    SpeedHorizontal = 0,
                    ProjectileTime = DateTime.Now.AddSeconds(-Setting.EnemyFrequency),
                    Health = Setting.EnemyHealth,
                    FullHealth = Setting.EnemyHealth
                };
                UFOitems.Add(enemy);
                oldEnemy = timeOld;
            }
        }

        /// <summary>Создание БОССа</summary>
        private void CreateBoss()
        {
            EnemyBossClass boss = new EnemyBossClass()
            {
                Width = Setting.EnemyBossWidth,
                Height = Setting.EnemyBossHeight,
                Top = 0,
                Left = Setting.Width * .3,
                SpeedVertical = Setting.EnemyBossSpeed,
                SpeedHorizontal = 0,
                ProjectileTime = DateTime.Now.AddSeconds(-Setting.EnemyBossFrequencyProjectile),
                Health = Setting.EnemyBossHealth,
                FullHealth = Setting.EnemyBossHealth
            };
            UFOitems.Add(boss);
        }

        /// <summary>Метод  перемещения всех НЛО объектов</summary>
        /// <param name="intervalSec">Время в секундах</param>
        private void MoveUFO(double intervalSec)
        {
            foreach (UFOClass ufo in UFOitems.ToArray())
            {
                double offsetVerticalEnemy = ufo.SpeedVertical * intervalSec;
                double offsetHorizontal = ufo.SpeedHorizontal * intervalSec;

                double _Top = ufo.Top + offsetVerticalEnemy;
                double _Left = ufo.Left + offsetHorizontal;
                if (ufo == Gamer)
                {
                    if (_Top < 0)
                        _Top = 0;
                    else if (_Top > Heidht - Gamer.Height)
                        _Top = Heidht - Gamer.Height;

                    if (_Left < 0)
                        _Left = 0;
                    else if (_Left > Width - Gamer.Width)
                        _Left = Width - Gamer.Width;
                    Gamer.Top = _Top;
                    Gamer.Left = _Left;
                }
                else
                {
                    if (_Top > Heidht || _Top < 0 || _Left < 0 || _Left > Width - ufo.Width)
                    {
                        UFOitems.Remove(ufo);
                        if (ufo is EnemyBossClass)
                        {
                            timer.Stop();
                            OnEndGame(EndGameEnum.Losing);
                        }
                    }
                    else
                    {
                        ufo.Top = _Top;
                        ufo.Left = _Left;
                    }
                }
            }
        }

        /// <summary>Выстрел игрока снарядом</summary>
        public void GamerPifMethod()
        {
            BulletGamerClass bulletgmr = new BulletGamerClass()
            {
                Width = Setting.GamerBulletWidth,
                Height = Setting.GamerBulletHeight,
                Left = Gamer.Left + Gamer.Width * .5,
                Top = Gamer.Top + Setting.GamerBulletHeight,
                SpeedVertical = -Setting.GamerBulletSpeed
            };
            OnSound(SoundEnum.gamerShot);

            UFOitems.Add(bulletgmr);
        }

        /// <summary>Выстрел Игрока ракетой по самому нижнему противнику</summary>
        public void GamerPafMethod()
        {
            //EnemyClass FirstEnemy = UFOitems.OfType<EnemyClass>().First();
            var enemies = UFOitems.OfType<EnemyClass>().ToArray();
            if (enemies == null || enemies.Length == 0)
                return;
            EnemyClass FirstEnemy = enemies
                .OrderBy(en => en.Top)
                .Last();

            double rTop = Gamer.Top - Setting.GamerRocketHeight;
            double rLeft = Gamer.Left + Gamer.Width * .5;
            double dTop = (FirstEnemy.Top + FirstEnemy.Height * .5) - rTop + Setting.EnemyHeight * .5;
            double dLeft = (FirstEnemy.Left + FirstEnemy.Width * .5) - rLeft;
            double dLenght = Math.Sqrt(dTop * dTop + dLeft * dLeft);
            double rAngle = dLeft > dTop ? Math.Atan(dTop / dLeft) : Math.PI * .5 - Math.Atan(dLeft / dTop);
            rAngle = 180 * rAngle / Math.PI;
            if (dLeft <= 0)
            {
                if (dTop >= 0)

                    rAngle += 180;
                else
                    rAngle -= 180;
            }
            double rSpeed = Setting.GamerRocketSpeed;
            double rSpeedV = (rSpeed * dTop) / dLenght;
            double rSpeedH = (rSpeed * dLeft) / dLenght;

            RocketGamerClass rocketgmr = new RocketGamerClass()
            {
                Width = Setting.GamerRocketWidth,
                Height = Setting.GamerRocketHeight,
                Left = rLeft,
                Top = rTop,
                SpeedVertical = rSpeedV,
                SpeedHorizontal = rSpeedH,
                Angle = rAngle - 90
            };
            OnSound(SoundEnum.gamerRocket);

            UFOitems.Add(rocketgmr);
        }

        /// <summary>Остановка игры на паузу</summary>
        public void Pause()
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
                timeOldDelta = DateTime.Now - timeOld;
                oldEnemyDelte = DateTime.Now - oldEnemy;
            }
        }

        /// <summary>Время прошедшее с последнего кадра</summary>
        private TimeSpan timeOldDelta;
        /// <summary>Время прошедшее с создания последнего противника</summary>
        private TimeSpan oldEnemyDelte;

        /// <summary>Продолжение игры после паузы</summary>
        public void Continue()
        {
            if (!timer.IsEnabled)
            {
                timeOld = oldEnemy = DateTime.Now;
                timeOld -= timeOldDelta;
                oldEnemy -= oldEnemyDelte;
                timer.Start();
            }
        }

    }
}
