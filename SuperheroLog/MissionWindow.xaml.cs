using SuperheroLog.Database;
using SuperheroLog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace SuperheroLog
{
    /// <summary>
    /// Interaction logic for MissionWindow.xaml
    /// </summary>
    public partial class MissionWindow : Window
    {
        public MissionWindow()
        {
            InitializeComponent();
        }

        SuperheroDataContext database = new();
        List<Character> characterList = new();
        public MissionDetailModel model;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            characterList = database.Characters.OrderBy(character => character.Alias).ToList();
            gridCharacter.ItemsSource = characterList;

            if (model != null && model.Id != 0)
            {
                txtCharacterNo.Text = model.CharacterNo.ToString();
                txtAlias.Text = model.Alias;
                txtMissionName.Text = model.MissionName;
                txtDescription.Text = model.MissionDescription;
            }
        }

        int CharacterId = 0;
        private void GridCharacter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Character character = (Character)gridCharacter.SelectedItem;
            txtCharacterNo.Text = character.CharacterNo.ToString();
            txtAlias.Text = character.Alias;
            CharacterId = character.Id;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

            if (txtMissionName.Text.Trim() == "" || txtDescription.Text.Trim() == "")
            {
                MessageBox.Show("Please fill in all the required fields.");
            }
            else 
            {
                if (model != null && model.Id != 0)
                {
                    Mission mission = database.Missions.Find(model.Id);

                    if (CharacterId != 0)
                    {
                        mission.CharacterId = CharacterId;
                    }

                    mission.MissionName = txtMissionName.Text;
                    mission.MissionDescription = txtDescription.Text;

                    database.SaveChanges();

                    MessageBox.Show("Mission was updated successfully.");
                }
                else
                {
                    if (CharacterId == 0)
                    {
                        MessageBox.Show("Please select a character from the table.");
                    }
                    else 
                    {
                        Mission mission = new();
                        mission.CharacterId = CharacterId;
                        mission.MissionName = txtMissionName.Text;
                        mission.MissionDescription = txtDescription.Text;
                        mission.MissionStatus = Definitions.MissionStatuses.Draft;

                        database.Missions.Add(mission);
                        database.SaveChanges();

                        MessageBox.Show("New mission was added successfully.");

                        CharacterId = 0;
                        txtCharacterNo.Clear();
                        txtAlias.Clear();
                        txtDescription.Clear();
                        txtMissionName.Clear();
                    }
                }
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
