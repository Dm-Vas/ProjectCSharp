using System.Windows;
using System.Windows.Controls;

namespace SuperheroLog.Views
{
    /// <summary>
    /// Interaction logic for MissionList.xaml
    /// </summary>
    public partial class MissionList : UserControl
    {
        public MissionList()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            MissionWindow window = new();
            window.ShowDialog();
        }
    }
}
