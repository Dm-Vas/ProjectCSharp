using Microsoft.EntityFrameworkCore;
using SuperheroLog.Database;
using SuperheroLog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
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
            FillDataGrid();
        }

        SUPERHEROBASEContext database = new();
        List<Team> teams = new();
        List<CharacterDetailModel> list = new();
        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            FillDataGrid();
        }

        private void CmbUniverse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int UniverseId = Convert.ToInt32(cmbUniverse.SelectedValue);

            if (cmbUniverse.SelectedIndex != -1)
            {
                cmbTeam.ItemsSource = teams.Where(team => team.UniverseId == UniverseId).ToList();
                cmbTeam.DisplayMemberPath = "TeamName";
                cmbTeam.SelectedValuePath = "Id";
                cmbTeam.SelectedIndex = -1;
            }
        }

        private void BtnSearch_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            List<CharacterDetailModel> searchlist = list;

            if (txtCharacterNo.Text.Trim() != "")
                searchlist = searchlist.Where(searchItem => searchItem.CharacterNo == Convert.ToInt32(txtCharacterNo.Text)).ToList();

            if (txtAlias.Text.Trim() != "")
                searchlist = searchlist.Where(searchItem => searchItem.Alias.ToLower().Contains(txtAlias.Text.ToLower())).ToList();

            if (txtName.Text.Trim() != "")
                searchlist = searchlist.Where(searchItem => searchItem.Name.ToLower().Contains(txtName.Text.ToLower())).ToList();

            if (txtSurname.Text.Trim() != "")
                searchlist = searchlist.Where(searchItem => searchItem.Surname.ToLower().Contains(txtSurname.Text.ToLower())).ToList();

            if (cmbUniverse.SelectedIndex != -1)
                searchlist = searchlist.Where(searchItem => searchItem.UniverseId == Convert.ToInt32(cmbUniverse.SelectedValue)).ToList();

            if (cmbTeam.SelectedIndex != -1)
                searchlist = searchlist.Where(searchItem => searchItem.TeamId == Convert.ToInt32(cmbTeam.SelectedValue)).ToList();

            gridCharacter.ItemsSource = searchlist;

        }

        private void BtnClear_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            txtCharacterNo.Clear();
            txtAlias.Clear();
            txtName.Clear();
            txtSurname.Clear();
            cmbUniverse.SelectedIndex = -1;
            cmbTeam.ItemsSource = teams;
            cmbTeam.SelectedIndex = -1;
            gridCharacter.ItemsSource = list;
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            CharacterDetailModel model = (CharacterDetailModel)gridCharacter.SelectedItem;

            if (model != null && model.Id != 0)
            {
                CharacterWindow windopw = new()
                {
                    model = model
                };
                windopw.ShowDialog();
                FillDataGrid();
            }
        }

        void FillDataGrid()
        {
            cmbUniverse.ItemsSource = database.Universes.ToList();
            cmbUniverse.DisplayMemberPath = "UniverseName";
            cmbUniverse.SelectedValuePath = "Id";
            cmbUniverse.SelectedIndex = -1;

            teams = database.Teams.ToList();
            cmbTeam.ItemsSource = teams;
            cmbTeam.DisplayMemberPath = "TeamName";
            cmbTeam.SelectedValuePath = "Id";
            cmbTeam.SelectedIndex = -1;

            list = database.Characters.Include(character => character.Team).Include(character => character.Universe).Select(character => new CharacterDetailModel()
            {
                Id = character.Id,
                CharacterNo = character.CharacterNo,
                Alias = character.Alias,
                Name = character.Name,
                Surname = character.Surname,
                Bio = character.Bio,
                UniverseId = character.UniverseId,
                UniverseName = character.Universe.UniverseName,
                TeamId = character.TeamId,
                TeamName = character.Team.TeamName,
                ImagePath = character.ImagePath,
            }).ToList();

            gridCharacter.ItemsSource = list;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            CharacterDetailModel model = (CharacterDetailModel)gridCharacter.SelectedItem;

            if (model != null && model.Id != 0) {
                if (MessageBox.Show("Are you sure you want to delete this character?",
                    "Question",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning)
                    ==
                    MessageBoxResult.Yes)
                {
                    Character character = database.Characters.Find(model.Id);
                    database.Characters.Remove(character);
                    database.SaveChanges();
                    MessageBox.Show("Character was deleted successfully");
                    FillDataGrid();
                }
            }
        }
    }
}
