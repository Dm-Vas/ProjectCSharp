using SuperheroLog.Database;
using System.Windows;

namespace SuperheroLog
{
    /// <summary>
    /// Interaction logic for UniverseWindow.xaml
    /// </summary>
    public partial class UniverseWindow : Window
    {
        public UniverseWindow()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtUniverseName.Text.Trim() == "")
                MessageBox.Show("Universe Name cannot be empty");
            else
            {
                using (SUPERHEROBASEContext database = new SUPERHEROBASEContext())
                {
                    Universe universe = new();
                    universe.UniverseName = txtUniverseName.Text;
                    database.Universes.Add(universe);
                    database.SaveChanges();
                    txtUniverseName.Clear();
                    MessageBox.Show("New Universe was added succesfully");
                }
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
