﻿using Microsoft.EntityFrameworkCore;
using SuperheroLog.Database;
using SuperheroLog.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SuperheroLog.Views
{
    /// <summary>
    /// Interaction logic for TeamList.xaml
    /// </summary>
    public partial class TeamList : UserControl
    {
        public TeamList()
        {
            InitializeComponent();
        }

        SUPERHEROBASEContext database = new();
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillGrid();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            TeamWindow window = new();
            this.Visibility = Visibility.Collapsed;
            window.ShowDialog();
            FillGrid();
            this.Visibility = Visibility.Visible;
        }

        void FillGrid()
        {
            var list = database.Teams.Include(team => team.Universe).Select(team => new {
                teamId = team.Id,
                teamName = team.TeamName,
                universeId = team.UniverseId,
                universeName = team.Universe.UniverseName
            }).OrderBy(team => team.teamName).ToList();

            List<TeamModel> modellist = new();

            foreach (var item in list)
            {
                TeamModel model = new()
                {
                    Id = item.teamId,
                    TeamName = item.teamName,
                    UniverseName = item.universeName,
                    UniverseId = item.universeId
                };
                modellist.Add(model);
            }
            gridTeam.ItemsSource = modellist;
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            TeamModel model = (TeamModel)gridTeam.SelectedItem;

            if (model != null && model.Id != 0)
            {
                TeamWindow window = new()
                {
                    model = model
                };
                window.ShowDialog();
                FillGrid();
            }
        }
    }
}
