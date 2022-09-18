using System.Windows.Controls;

namespace SuperheroLog.Views
{
    /// <summary>
    /// Interaction logic for CharacterList.xaml
    /// </summary>
    public partial class CharacterList : UserControl
    {
        public CharacterList()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CharacterWindow windwow = new();
            windwow.ShowDialog();
        }
    }
}
