using SuperheroLog.Database;
using SuperheroLog.Views;
using System.Collections.Generic;
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

        public Universe universe;
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if
            (txtUniverseName.Text.Trim() == "")
            {
                MessageBox.Show("Universe Name cannot be empty.");
            }
            else
            {
                using SuperheroDataContext database = new();

                if (universe != null && universe.Id != 0)
                {
                    Universe update = new()
                    {
                        UniverseName = txtUniverseName.Text,
                        Id = universe.Id
                    };

                    database.Universes.Update(update);
                    database.SaveChanges();

                    MessageBox.Show("Universe was updated succesfully.");
                }
                else
                {
                    Universe universe = new()
                    {
                        UniverseName = txtUniverseName.Text
                    };

                    database.Universes.Add(universe);
                    database.SaveChanges();

                    txtUniverseName.Clear();
                    MessageBox.Show("New Universe was added succesfully.");
                }
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (universe != null && universe.Id != 0)
            {
                txtUniverseName.Text = universe.UniverseName;
            }
        }
    }
}
