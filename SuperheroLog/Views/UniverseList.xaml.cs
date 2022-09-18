using SuperheroLog.Database;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SuperheroLog.Views
{
    /// <summary>
    /// Interaction logic for UniverseList.xaml
    /// </summary>
    public partial class UniverseList : UserControl
    {
        public UniverseList()
        {
            InitializeComponent();
            using SUPERHEROBASEContext database = new();
            List<Universe> list = database.Universes.OrderBy(univerce => univerce.UniverseName).ToList();
            gridUniverse.ItemsSource = list;
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            UniverseWindow window = new();
            this.Visibility = Visibility.Collapsed;
            window.ShowDialog();
            using SUPERHEROBASEContext database = new();
            List<Universe> list = database.Universes.OrderBy(universe => universe.UniverseName).ToList();
            gridUniverse.ItemsSource = list;
            this.Visibility = Visibility.Visible;
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Universe universe = (Universe)gridUniverse.SelectedItem;
            UniverseWindow window = new()
            {
                universe = universe
            };
            this.Visibility = Visibility.Collapsed;
            window.ShowDialog();
            using SUPERHEROBASEContext database = new();
            gridUniverse.ItemsSource = database.Universes.OrderBy(universe => universe.UniverseName).ToList();
            this.Visibility = Visibility.Visible;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Universe universe = (Universe)gridUniverse.SelectedItem;

            if (universe != null && universe.Id != 0)
            {
                if (MessageBox.Show("Are you sure you want to delete this item?", 
                    "Question", 
                    MessageBoxButton.YesNo, 
                    MessageBoxImage.Warning) 
                    == 
                    MessageBoxResult.Yes)
                {
                    SUPERHEROBASEContext database = new();

                    List<Team> teams = database.Teams.Where(team => team.UniverseId == universe.Id).ToList();
                    foreach (var item in teams)
                    {
                        database.Teams.Remove(item);
                    }
                    database.SaveChanges();

                    Universe department = database.Universes.Find(universe.Id);
                    database.Universes.Remove(department);
                    database.SaveChanges();
                    MessageBox.Show("Item was deleted successfully");
                    gridUniverse.ItemsSource = database.Universes.OrderBy(universe => universe.UniverseName).ToList();
                }
            }
        }
    }
}
