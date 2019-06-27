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

namespace Invasion_of_Gooks
{
    public partial class MainWindow : Window
    {
        //SQLiteConnection m_dbConnection;

        public bool gameplay = false;

        private MediaPlayer mainPlayer = new MediaPlayer();

        ViewModelClass viewModel;
        //DataGamer dataGamer;
        //Sky warSky;

        public MainWindow()
        {
            InitializeComponent();


        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            //viewModel.dataGamer.Name = nameTxt.Text;
            //viewModel.StartMetod(nameTxt.Text);
            viewModel.NameGamer = nameTxt.Text;
            viewModel.ResetValue();
            gameplay = true;
            mainPlayer.Stop();
            mainPlayer.Close();
            BattleWind battle = new BattleWind();
            //warSky.IsEndGame = false;
            this.Hide();

            battle.Owner = this;

            battle.Show();



            //this.Close();
            //this.Show();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //using (SQLiteConnection connection = new SQLiteConnection(@"Data Source=C:\Users\Admin\Desktop\iog.db"))
            //using (SQLiteConnection connection = new SQLiteConnection(@"Data Source=C:\Users\Администратор\Desktop\FULL\iog.db"))
            //{
            //    connection.Open();
            //    SQLiteCommand command = new SQLiteCommand("Select * from kyrsach", connection);
            //    SQLiteDataReader reader = command.ExecuteReader();

            //    //string sql = "SELECT * FROM kyrsach ORDER BY Name";

            //    while (reader.Read())
            //    {
            //        var dt = new DataGamer { Name = reader["Name"].ToString(), Scr = int.Parse(reader["Score"].ToString()) };
            //        data.Items.Add(dt);
            //    }
            //    data.IsEnabled = true;

            //    connection.Close();
            //}
            data.ItemsSource = ModelDataBaseClass.DataGamers;
        }

        private void NameTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            Start.IsEnabled = !String.IsNullOrWhiteSpace(nameTxt.Text);
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            
            viewModel = (ViewModelClass)DataContext;

            //=====================================================
            //viewModel.ResetValue();
            //=====================================================
            //warSry = (Sky)DataContext;
            mainPlayer.Open(new Uri("C:/Users/Admin/Desktop/FULL/Invasion of Gooks/Resources/Sound/menuSingle.wav", UriKind.Absolute));
            mainPlayer.Play();
        }

        //public void ScoreBase(int score)
        //{
        //    string sql = "INSERT INTO kyrsach (Name, Score) VALUES (" + "'" + name + "' ," + score + ")";
        //    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);

        //    //var dt = new DataBase1 { name = name.Text, scr =  };
        //    //data.Items.Add(dt);
        //}
    }
}
