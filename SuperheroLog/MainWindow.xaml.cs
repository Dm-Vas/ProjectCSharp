using SuperheroLog.ViewModels;
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
            lblWindowName.Content = "List of Characters";
            DataContext = new CharacterViewModel();
        }

        private void BtnCharacters_Click(object sender, RoutedEventArgs e)
        {
            lblWindowName.Content = "List of Characters";
            DataContext = new CharacterViewModel();
        }

        private void BtnUniverses_Click(object sender, RoutedEventArgs e)
        {
            lblWindowName.Content = "List of Universes";
            DataContext = new UniverseViewModel();
        }

        private void BtnTeams_Click(object sender, RoutedEventArgs e)
        {
            lblWindowName.Content = "List of Teams";
            DataContext = new TeamViewModel();
        }

        private void BtnMissions_Click(object sender, RoutedEventArgs e)
        {
            lblWindowName.Content = "List of Missions";
            DataContext = new MissionViewModel();
        }
    } 
}
