using Invasion_of_Gooks.Properties;
using Invasion_of_Gooks.View;
using InvasionViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Media;
using System.Resources;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Invasion_of_Gooks
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        readonly MainWindow mainWindow;
        readonly ViewModelBattleClass viewModelBattle;
        readonly ViewModelDataBaseClass viewModelDataBase;

        readonly StartUC startUC;
        readonly BattleUC battleUC;

        /// <summary>Поле для блокировки "нелегального" закрытия окна</summary>
        private bool isExit = false;

        public App()
        {
            /// Создание VM, View и их связывание
            viewModelDataBase = new ViewModelDataBaseClass
                (
                    StartMetod,
                    (p) => true,
                    ExitMetod,
                    (p) => true
                );
            viewModelBattle = new ViewModelBattleClass
                (
                    MainMetod,
                    (p) => true
                );
            mainWindow = new MainWindow();
            startUC = new StartUC() { DataContext = viewModelDataBase };
            battleUC = new BattleUC() { DataContext = viewModelBattle };
            mainWindow.Closing += MainWindow_Closing;

            /// Пауза звукового фона до загрузки окна
            MediaPlayerExtensions.AllPause();

            /// Продолжение проигрывания всех плееров в ктивном окне
            mainWindow.Activated += (s, e) => MediaPlayerExtensions.AllContinue();
            /// Остановка всех плееров в неактивном окне
            mainWindow.Deactivated += (s, e) => MediaPlayerExtensions.AllPause();

        }

        /// <summary>Метод для перехода к отображению Главного меню</summary>
        /// <param name="parameter"></param>
        private void MainMetod(object parameter = null)
        {
            mainWindow.Content = startUC;

            MediaPlayerEnum.MainWindow.Play();
            MediaPlayerEnum.RideOfTheValkyries.Pause();
            MediaPlayerEnum.Propeller1.Pause();
        }

        /// <summary>Метод Главного меню для старта игры</summary>
        /// <param name="parameter"></param>
        private void StartMetod(object parameter = null)
        {
            mainWindow.Content = battleUC;

            MediaPlayerEnum.MainWindow.Stop();
            MediaPlayerEnum.RideOfTheValkyries.Play();
            MediaPlayerEnum.Propeller1.Play();
            if (viewModelBattle.IsPauseKey)
                MediaPlayerEnum.Propeller1.Pause();
        }

        /// <summary>Обработка попытки закрытия окна</summary>
        /// <remarks>Закрытие окна разрешается если установлено
        /// поле isExit</remarks>
        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = !isExit;
        }

        /// <summary>Старт приложения</summary>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainMetod();
            mainWindow.Show();
        }

        /// <summary>Метод "легального" закрытия окна</summary>
        private void ExitMetod(object parameter)
        {
            isExit = true;
            mainWindow.Close();
        }
    }
}
