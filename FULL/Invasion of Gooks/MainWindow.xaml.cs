using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Invasion_of_Gooks.View;
using System.Data.SQLite;
using Invasion_of_Gooks.Model;
using Invasion_of_Gooks.ViewModel;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace Invasion_of_Gooks
{
    public partial class MainWindow : Window
    {
        //SQLiteConnection m_dbConnection;

        public bool gameplay = false;

        //private MediaPlayer bangPlayer = new MediaPlayer();
        //private MediaPlayer mainPlayer = new MediaPlayer();

        //ViewModelClass viewModel;
        //DataGamer dataGamer;
        //Sky warSky;

        public MainWindow()
        {
            InitializeComponent();

            //MediaPlayerEnum.MainWindow.Play();

            //bangPlayer.Open(new Uri(@"C:\Users\EldHa\Мои проекты\Максим\Invasion-of-gooks\FULL\Invasion of Gooks\bin\Debug\Resources\Sound\bang.wav", UriKind.Absolute));
            //mainPlayer.Open(new Uri(@"C:\Users\EldHa\Мои проекты\Максим\Invasion-of-gooks\FULL\Invasion of Gooks\bin\Debug\Resources\Sound\menuSingle.wav", UriKind.Absolute));
            //mainPlayer.Play();
            //MediaPlayerExtensions.Players[MediaPlayerEnum.MainWindow].Play();

        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            //bangPlayer.Position = TimeSpan.Zero;
            //bangPlayer.Play();

            //MediaPlayerExtensions.Players[MediaPlayerEnum.bang].Position = TimeSpan.Zero;
            //MediaPlayerExtensions.Players[MediaPlayerEnum.bang].Play();


            //MediaPlayerEnum.MainWindow.Play();

            //MediaPlayerEnum.bang.Play(true);


            ////viewModel.dataGamer.Name = nameTxt.Text;
            ////viewModel.StartMetod(nameTxt.Text);
            ////viewModel.NameGamer = nameTxt.Text;
            ////viewModel.ResetValue();
            //gameplay = true;
            //mainPlayer.Stop();
            //mainPlayer.Close();
            //BattleWind battle = new BattleWind();
            ////warSky.IsEndGame = false;
            //this.Hide();

            //battle.Owner = this;

            //battle.Show();



            ////this.Close();
            ////this.Show();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    //using (SQLiteConnection connection = new SQLiteConnection(@"Data Source=C:\Users\Admin\Desktop\iog.db"))
        //    //using (SQLiteConnection connection = new SQLiteConnection(@"Data Source=C:\Users\Администратор\Desktop\FULL\iog.db"))
        //    //{
        //    //    connection.Open();
        //    //    SQLiteCommand command = new SQLiteCommand("Select * from kyrsach", connection);
        //    //    SQLiteDataReader reader = command.ExecuteReader();

        //    //    //string sql = "SELECT * FROM kyrsach ORDER BY Name";

        //    //    while (reader.Read())
        //    //    {
        //    //        var dt = new DataGamer { Name = reader["Name"].ToString(), Scr = int.Parse(reader["Score"].ToString()) };
        //    //        data.Items.Add(dt);
        //    //    }
        //    //    data.IsEnabled = true;

        //    //    connection.Close();
        //    //}
        //    //data.ItemsSource = ModelDataBaseClass.DataGamers;
        //}

        private void NameTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Start.IsEnabled = !String.IsNullOrWhiteSpace(nameTxt.Text);
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {

            //viewModel = (ViewModelClass)DataContext;

            //=====================================================
            //viewModel.ResetValue();
            //=====================================================
            //warSry = (Sky)DataContext;
            //mainPlayer.Open(new Uri("C:/Users/Admin/Desktop/FULL/Invasion of Gooks/Resources/Sound/menuSingle.wav", UriKind.Absolute));
            //mainPlayer.Play();
            //MediaPlayerEnum.MainWindow.Play();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            //MediaPlayerEnum.MainWindow.Continue();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            MediaPlayerEnum.MainWindow.Pause();
        }

        //public void ScoreBase(int score)
        //{
        //    string sql = "INSERT INTO kyrsach (Name, Score) VALUES (" + "'" + name + "' ," + score + ")";
        //    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);

        //    //var dt = new DataBase1 { name = name.Text, scr =  };
        //    //data.Items.Add(dt);
        //}
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;

            if (hwndSource != null)
            {
                hwndSource.AddHook(HwndSourceHook);
            }
        }
        private bool allowClosing = false;

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32.dll")]
        private static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        private const uint MF_BYCOMMAND = 0x00000000;
        private const uint MF_GRAYED = 0x00000001;

        private const uint SC_CLOSE = 0xF060;

        private const int WM_SHOWWINDOW = 0x00000018;
        private const int WM_CLOSE = 0x10;

        private IntPtr HwndSourceHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_SHOWWINDOW:
                    {
                        IntPtr hMenu = GetSystemMenu(hwnd, false);
                        if (hMenu != IntPtr.Zero)
                        {
                            EnableMenuItem(hMenu, SC_CLOSE, MF_BYCOMMAND | MF_GRAYED);
                        }
                    }
                    break;
                case WM_CLOSE:
                    if (!allowClosing)
                    {
                        handled = true;
                    }
                    break;
            }
            return IntPtr.Zero;
        }

        public new void Close()
        {
            allowClosing = true;
            base.Close();
        }

        private void Frame_LoadCompleted(object sender, NavigationEventArgs e)
        {
           ((FrameworkElement) ((ContentControl)sender).Content).DataContext = ((ContentControl)sender).DataContext;
        }
    }
}
