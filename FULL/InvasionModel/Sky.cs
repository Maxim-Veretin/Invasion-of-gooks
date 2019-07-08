﻿using CommLibrary;
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
        /// <summary>количество yбитых врагов</summary>
        public int Frags
        {
            get => _frags; private set
            {
                if (_frags < 10 && value > 9)
                    CreateBoss();
                _frags = value;
                OnPropertyChanged();
            }
        }
        //public bool haveBoss = false;
        /// <summary>Набранные очки</summary>
        public int Score { get => score; set { score = value; OnPropertyChanged(); } }
        //public bool bossDie = false;

        //public MediaPlayer enemyPlayer = new MediaPlayer();

        /// <summary>Параметры неба</summary>
        public SkySetting Setting { get; }

        /// <summary>Событие вызова звука</summary>
        public event SoundHandler SoundEvent;

        /// <summary>Вспомогательный метод для вызова события звука</summary>
        /// <param name="sound"></param>
        private void OnSound(SoundEnum sound) => SoundEvent?.Invoke(this, sound);

        /// <summary>Событие конец игры</summary>
        public event EndGameHandler EndGameEvent;

        //public event ExplosionHandler ExplosionEvent;

        /// <summary>Вспомогательный метод для вызова события конец игры</summary>
        private void OnEndGame(EndGameEnum endGame)
        {
            IsEndGame = true;
            EndGameEvent?.Invoke(this, endGame);
        }

        //private void OnExplosion(double top, double left, double width, double height)
        //{
        //    ExplosionEvent?.Invoke(this, top, left, width, height);
        //}

        /// <summary>Ширина неба</summary>
        public double Width { get; }
        /// <summary>Ширина неба</summary>
        public double Heidht { get; }
        /// <summary>Пропорции неба</summary>
        public double Ratio { get; }

        ///// <summary>Игра закончена</summary>
        public bool IsEndGame { get; private set; }

        /// <summary>Объект игрока</summary>
        public GamerClass Gamer { get; }

        /// <summary>Объект босса</summary>
        public EnemyBossClass Boss { get; }

        /// <summary>Частота появления противников в секyндах</summary>
        public double Frequency { get; private set; }

        /// <summary>Частота появления снарядов в секyндах</summary>
        public double FrequencyProjectile { get; private set; }
        /// <summary>Доля ракет в выстрелах</summary>
        public double ShareRockets { get; private set; }

        /// <summary>все объекты в небе</summary>
        public ObservableCollection<UFOClass> UFOitems { get; } = new ObservableCollection<UFOClass>();

        /*private readonly*/
        public DispatcherTimer timer = new DispatcherTimer();
        private DateTime timeOld;

        //public View.BattleWind bat;

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
            //UFOitems.Add(new ExplosionClass() { Width = Width, Height = Heidht });

            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += Timer_Tick;
            timeOld = DateTime.Now;

            Frequency = Setting.EnemyFrequency;
            FrequencyProjectile = Setting.EnemyFrequencyProjectile;
            ShareRockets = Setting.EnemyShareRockets;

            oldEnemy = timeOld;
            //oldProjecileEnemy = timeOld;

            //timer.Start();
        }

        /// <summary>время создания предыдyщего врага</summary>
        private DateTime oldEnemy;

        //private DateTime spawnBooss;

        /// <summary>время создания предыдyщего cнаряда</summary>
        //private DateTime oldProjecileEnemy;
        private int _frags = 0;
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
            FrameMetodk(intervalFrame.TotalMilliseconds * 0.001);
            if (IsEndGame)
                return;
            timer.Start();
        }

        private void FrameMetodk(double intervalSec)
        {
            //if (!IsEndGame)
            //{
            MoveUFO(intervalSec);
            ProjectiliesEnemies();
            HittingGamer();
            HittingEnemies();
            CreateEnemies();
            ExplosionRemove();
            //if ((Frags >= 10) && (haveBoss == false))
            //{
            //    CreateBoss();
            //}
            //}
        }

        /// <summary>Расчёт попаданий по игроку</summary>
        public void HittingGamer()
        {
            Type[] typeHitting = { typeof(EnemyClass), typeof(BulletEnemyClass), typeof(RocketEnemyClass) };
            UFOClass[] hittUFO = UFOitems
                .Where(it => typeHitting.Contains(it.GetType()))
                .Where(it => Gamer.Intersection(it))
                .ToArray();

            foreach (UFOClass hUFO in hittUFO)
            {
                if (hUFO is HeliopterClass enemy)
                {
                    UFOitems.Remove(enemy);
                    Gamer.Health -= 5;
                }
                else if (hUFO is RocketEnemyClass rocket)
                {
                    UFOitems.Remove(rocket);
                    Gamer.Health -= 3;
                }
                else if (hUFO is BulletEnemyClass bullet)
                {
                    UFOitems.Remove(bullet);
                    Gamer.Health -= 1;
                }
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
            UFOClass[] hittUFO = UFOitems
                .OfType<ProjectileGamerClass>()
                .ToArray();
            UFOClass[] enemies = UFOitems
                .OfType<EnemyClass>()
                .ToArray();

            foreach (EnemyClass enemy in enemies)
            {
                foreach (UFOClass hUFO in hittUFO.Where(it => it.Intersection(enemy)))
                {
                    if (hUFO is RocketGamerClass rocket)
                    {
                        UFOitems.Remove(rocket);
                        enemy.Health -= 3;
                    }
                    else if (hUFO is BulletGamerClass bullet)
                    {
                        UFOitems.Remove(bullet);
                        enemy.Health -= 1;
                    }
                    if (enemy.Health <= 0)
                    {
                        Frags++;
                        OnSound(SoundEnum.enemyDie);
                        //OnExplosion(enemy.Top, enemy.Left, enemy.Width, enemy.Heidht);
                        if (enemy is EnemyBossClass)
                        {
                            Score += 200;
                            //bossDie = true;

                            //UFOitems.Remove(enemy);
                            //timer.Stop();
                            OnEndGame(EndGameEnum.Win);
                        }
                        else
                        {
                            Score += 20;

                            //UFOitems.Remove(enemy);
                        }
                        UFOitems.Remove(enemy);
                        //if (enemy is EnemyBossClass)
                        //    OnEndGame(EndGameEnum.Win);
                        //else
                        //{
                        ExplosionClass explosion = enemy.Copy<ExplosionClass>();
                        explosion.SpeedHorizontal = 0;
                        explosion.SpeedVertical = 0;

                        UFOitems.Add(explosion);
                        //}
                        //if (bossDie == true)
                        //    OnEndGame(EndGameEnum.Win);
                        //else if((bossDie==false)&&(int.Parse((DateTime.Now-spawnBooss).Seconds.ToString())==41))
                        //    OnEndGame(EndGameEnum.Losing);

                        return;
                    }
                }
            }
        }

        /// <summary>Удаление взорвавшихся взрывов</summary>
        private void ExplosionRemove()
        {
            foreach (ExplosionClass explosion in UFOitems.OfType<ExplosionClass>().Where(item => item.IsRemove).ToArray())
            {
                UFOitems.Remove(explosion);
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
            //haveBoss = true;
            //spawnBooss = DateTime.Now;
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

        public void Pause()
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
                timeOldDelta = DateTime.Now - timeOld;
                oldEnemyDelte = DateTime.Now - oldEnemy;
            }
        }

        private TimeSpan timeOldDelta, oldEnemyDelte;
        private int score;

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

        //public void AnimationTimer_Tick(object sender, EventArgs e)
        //{
        //    currentFrame = currentFrame + 1;
        //    var frameLeft = currentFrame * frameW;
        //    var frameTop = animationIndex * frameH;
        //    (heli.Fill as ImageBrush).Viewbox = new Rect(frameLeft, frameTop, frameLeft + frameW, frameTop + frameH);

        //    if (currentFrame == animations[animationIndex] - 5) //-1 номер кадра с которого начинается.
        //    {
        //        animationIndex++;
        //        if (animationIndex == animations.Length) animationIndex = 0;
        //        frameCount = animations[animationIndex];
        //        currentFrame = 0;
        //    }
        //}

        //public void DramBang(EnemyClass enemy, Grid scene)
        //{
        //    Rectangle myrect = new Rectangle();
        //    ImageBrush ib = new ImageBrush();
        //    ib.AlignmentX = AlignmentX.Left;
        //    ib.AlignmentY = AlignmentY.Top;
        //    ib.Stretch = Stretch.None;
        //    ib.ViewboxUnits = BrushMappingMode.Absolute;
        //    ImageBrush ib1 = new ImageBrush();
        //    ib.Viewbox = new Rect(0, 0, 300, 381);
        //    //ib.Viewbox = new Rect(enemy.Top, enemy.Left, enemy.Width, enemy.Heidht);
        //    ib.ViewboxUnits = BrushMappingMode.Absolute;
        //    heli.Fill = ib;
        //    heli.Height = frameH;
        //    heli.Width = frameW;

        //    //ib.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/Resources/vz.png", UriKind.Absolute));
        //    ib.ImageSource = new BitmapImage(new Uri("C:/Users/Admin/Desktop/Invasion of Gooks_фреймы/Invasion of Gooks/Resources/vz.png", UriKind.Absolute));

        //    heli.Fill = ib;
        //    heli.Height = enemy.Heidht;
        //    heli.Width = enemy.Width;
        //    myrect.Fill = ib;
        //    myrect.StrokeThickness = 0;
        //    myrect.Stroke = Brushes.Black;
        //    myrect.Height = enemy.Heidht;
        //    myrect.Width = enemy.Width;
        //    //myrect.RenderTransform = new TranslateTransform(0, 0); 
        //    scene.Children.Add(heli);

        //    ImageBrush fight_background = new ImageBrush
        //    {
        //        ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/in/vz.png", UriKind.Absolute))
        //    };
        //    myrect.Fill = fight_background;

        //    frameCount = animations[animationIndex];

        //    //timer.Tick += new EventHandler(AnimationTimer_Tick);
        //    //timer.Interval = new TimeSpan(0, 0, 0, 0, 50);
        //    //timer.Start();
        //    animation.Start();
        //}

        //public void ScoreBase(int score)
        //{
        //    string sql = "INSERT INTO kyrsach (Name, Score) VALUES (" + "'" + name + "' ," + score + ")";
        //    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);

        //    var dt = new DataBase1 { name = name, scr = int.Parse(reader["Score"].ToString()) };
        //    data.Items.Add(dt);
        //}
    }
}
