using SuperheroLog.Database;
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

        SUPERHEROBASEContext database = new();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var list = database.Universes.ToList().OrderBy(universe => universe.UniverseName);

            cmbUniverse.ItemsSource = list;
            cmbUniverse.DisplayMemberPath = "UniverseName";
            cmbUniverse.SelectedValuePath = "Id";
            cmbUniverse.SelectedIndex = -1;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cmbUniverse.SelectedIndex == -1 || txtTeam.Text.Trim() == "")
                MessageBox.Show("Please fill in all fields");
            else
            {
                Team team = new()
                {
                    TeamName = txtTeam.Text,
                    UniverseId = Convert.ToInt32(cmbUniverse.SelectedValue)
                };
                database.Teams.Add(team);
                database.SaveChanges();
                cmbUniverse.SelectedIndex = -1;
                txtTeam.Clear();
                MessageBox.Show("New Team was added successfully");
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
