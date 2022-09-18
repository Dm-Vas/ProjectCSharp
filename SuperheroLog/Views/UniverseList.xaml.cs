using SuperheroLog.Database;
using System.Collections.Generic;
using System.Linq;
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
    }
}
