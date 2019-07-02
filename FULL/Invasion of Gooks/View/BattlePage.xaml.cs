﻿using InvasionViewModel;
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

namespace Invasion_of_Gooks.View
{
    /// <summary>
    /// Логика взаимодействия для BattlePage.xaml
    /// </summary>
    public partial class BattlePage : Page
    {
        ViewModelBattleClass ViewModelBattle { get; }
        public BattlePage(ViewModelBattleClass viewModelBattle)
        {
            InitializeComponent();
            ViewModelBattle = viewModelBattle;
            DataContext = ViewModelBattle;
        }
    }
}