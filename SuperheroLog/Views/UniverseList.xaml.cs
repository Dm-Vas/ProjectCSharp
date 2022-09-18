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
            using SUPERHEROBASEContext databse = new();
            List<Universe> list = databse.Universes.OrderBy(univerce => univerce.UniverseName).ToList();
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
    }
}
