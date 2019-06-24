using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;

namespace Invasion_of_Gooks.Model
{
    /// <summary>Перечисление типов звуков</summary>
    public enum SoundEnum
    {
        /// <summary>Смерть противника</summary>
        enemyDie,
        /// <summary>Выстрел противника</summary>
        enemyShot,
        /// <summary>Выстрел игрока</summary>
        gamerShot,
        /// <summary>Пуск ракеты</summary>
        gamerRocket
    }

    /// <summary>Причины конца игры</summary>
    public enum EndGameEnum
    {
        /// <summary>Победа</summary>
        Win,
        /// <summary>Проигрыш</summary>
        Losing
    }

    public delegate void SoundHandler(Sky sky, SoundEnum sound);
    public delegate void EndGameHandler(Sky sky, EndGameEnum endGame);
    
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
    }

    /// <summary>Класс неба</summary>
    public class Sky
    {
        public int frags = 0;

        public MediaPlayer enemyPlayer = new MediaPlayer();

        /// <summary>Парметры неба</summary>
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
        /// <summary>Ширина неба</summary>
        public double Heidht { get; }
        /// <summary>Пропорции неба</summary>
        public double Ratio { get; }

        /// <summary>Игра закончена</summary>
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

        /*private readonly*/
        public DispatcherTimer timer = new DispatcherTimer();
        private DateTime timeOld;

        public Sky(SkySetting setting)
        {
            Setting = setting;
            Width = Setting.Width;
            Heidht = Setting.Height;
            Ratio = Width / Heidht;

            //создание игрока
            Gamer = new GamerClass(Setting.GamerWidth, Setting.GamerHeight)
            {
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
            oldProjecileEnemy = timeOld;

            timer.Start();
        }

        /// <summary>время создания предыдyщего врага</summary>
        private DateTime oldEnemy;

        /// <summary>время создания предыдyщего cнаряда</summary>
        private DateTime oldProjecileEnemy;

        private static readonly Random random = new Random();

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            if (IsEndGame)
                return;
            //делаем так, потомy что больше timeOld не нyжна 
            DateTime timeNew = DateTime.Now;
            TimeSpan intervalFrame = timeNew - timeOld;
            timeOld = timeNew;
            FrameMetodk(intervalFrame.TotalMilliseconds * 0.001);
            timer.Start();
        }

        private void FrameMetodk(double intervalSec)
        {
            MoveUFO(intervalSec);
            CreateEnemies();
            ProjectiliesEnemies();
            HittingGamer();
            HittingEnemies();
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
                        frags++;
                        OnSound(SoundEnum.enemyDie);
                        //enemyPlayer.Open(new)
                        UFOitems.Remove(enemy);

                        if (frags == 10)
                            
                            OnEndGame(EndGameEnum.Win);

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
                    projectile = new BulletEnemyClass(Setting.EnemyBulletWidth, Setting.EnemyBulletHeight)
                    {
                        Left = enemy.Left + enemy.Width * .5,
                        Top = enemy.Top + enemy.Heidht,
                        SpeedVertical = Setting.EnemyBulletSpeed
                    };
                }
                else
                {
                    // Выстрел ракетой
                    double rTop = enemy.Top + enemy.Heidht;
                    double rLeft = enemy.Left + enemy.Width * .5;
                    double dTop = (Gamer.Top + Gamer.Heidht * .5) - rTop;
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
                    projectile = new RocketEnemyClass(Setting.EnemyRocketWidth, Setting.EnemyRocketHeight)
                    {
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
                EnemyClass enemy = new EnemyClass(Gamer.Width, Gamer.Heidht)
                {
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
                    else if (_Top > Heidht - Gamer.Heidht)
                        _Top = Heidht - Gamer.Heidht;

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
                        UFOitems.Remove(ufo);
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
            BulletGamerClass bulletgmr = new BulletGamerClass(Setting.GamerBulletWidth, Setting.GamerBulletHeight)
            {
                Left = Gamer.Left + Gamer.Width * .5,
                Top = Gamer.Top + Setting.GamerBulletHeight,
                SpeedVertical = -Setting.GamerBulletSpeed
            };

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
            double dTop = (FirstEnemy.Top + FirstEnemy.Heidht * .5) - rTop + Setting.EnemyHeight * .5;
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

            RocketGamerClass rocketgmr = new RocketGamerClass(Setting.GamerRocketWidth, Setting.GamerRocketHeight)
            {
                Left = rLeft,
                Top = rTop,
                SpeedVertical = rSpeedV,
                SpeedHorizontal = rSpeedH,
                Angle = rAngle - 90
            };
            UFOitems.Add(rocketgmr);

            
        }

        public void Pause() => timer.Stop();

        public void Continue()
        {
            timeOld = oldEnemy = DateTime.Now;
            timer.Start();
        }
    }
}
