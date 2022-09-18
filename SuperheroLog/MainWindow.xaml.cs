﻿using SuperheroLog.ViewModels;
using System.Windows;

namespace SuperheroLog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnCharacters_Click(object sender, RoutedEventArgs e)
        {
            lblWindowName.Content = "Character List";
            DataContext = new CharacterViewModel();
        }

        private void BtnUniverses_Click(object sender, RoutedEventArgs e)
        {
            lblWindowName.Content = "Universe List";
            DataContext = new UniverseViewModel();
        }

        private void BtnTeams_Click(object sender, RoutedEventArgs e)
        {
            lblWindowName.Content = "Team List";
            DataContext = new TeamViewModel();
        }
    }
}
