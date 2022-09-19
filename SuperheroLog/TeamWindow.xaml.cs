using SuperheroLog.Database;
using SuperheroLog.ViewModels;
using System;
using System.Linq;
using System.Windows;

namespace SuperheroLog
{
    /// <summary>
    /// Interaction logic for TeamWindow.xaml
    /// </summary>
    public partial class TeamWindow : Window
    {
        public TeamWindow()
        {
            InitializeComponent();
        }

        SuperheroDataContext database = new();
        public TeamModel model;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var list = database.Universes.ToList().OrderBy(universe => universe.UniverseName);

            cmbUniverse.ItemsSource = list;
            cmbUniverse.DisplayMemberPath = "UniverseName";
            cmbUniverse.SelectedValuePath = "Id";
            cmbUniverse.SelectedIndex = -1;

            if (model != null && model.Id != 0)
            {
                cmbUniverse.SelectedValue = model.UniverseId;
                txtTeam.Text = model.TeamName;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cmbUniverse.SelectedIndex == -1 || txtTeam.Text.Trim() == "")
                MessageBox.Show("Please fill in all fields.");
            else
            {

                if (model != null && model.Id != 0)
                {
                    Team team = new()
                    {
                        UniverseId = (int)cmbUniverse.SelectedValue,
                        Id = model.Id,
                        TeamName = txtTeam.Text
                    };
                    database.Teams.Update(team);
                    database.SaveChanges();
                    MessageBox.Show("Team was updated successfully.");
                }
                else {
                    Team team = new()
                    {
                        TeamName = txtTeam.Text,
                        UniverseId = Convert.ToInt32(cmbUniverse.SelectedValue)
                    };
                    database.Teams.Add(team);
                    database.SaveChanges();
                    cmbUniverse.SelectedIndex = -1;
                    txtTeam.Clear();
                    MessageBox.Show("New Team was added successfully.");
                }
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
