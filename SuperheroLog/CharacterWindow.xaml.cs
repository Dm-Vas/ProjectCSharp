using Microsoft.Win32;
using SuperheroLog.Database;
using SuperheroLog.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SuperheroLog
{
    /// <summary>
    /// Interaction logic for CharacterWindow.xaml
    /// </summary>
    public partial class CharacterWindow : Window
    {
        public CharacterWindow()
        {
            InitializeComponent();
        }
        SuperheroDataContext database = new();
        List<Team> teams = new();
        public CharacterDetailModel model;
        private void Window_Loaded(object sender, RoutedEventArgs e)
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

            if (model != null && model.Id != 0)
            {
                txtCharacterNo.Text = model.CharacterNo.ToString();
                txtAlias.Text = model.Alias;
                txtName.Text = model.Name;
                txtSurname.Text = model.Surname;
                cmbUniverse.SelectedValue = model.UniverseId;
                cmbTeam.SelectedValue = model.TeamId;
                txtBio.AppendText(model.Bio);
                BitmapImage image = new();
                image.BeginInit();
                image.UriSource = new Uri(@"Images/" + model.ImagePath, UriKind.RelativeOrAbsolute);
                image.EndInit();
                CharacterImage.Source = image;
            }
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
        private void TxtCharacterNo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        OpenFileDialog dialog = new();
        private void BtnUpload_Click(object sender, RoutedEventArgs e)
        {
            if (dialog.ShowDialog() == true)
            {
                BitmapImage image = new();
                image.BeginInit();
                image.UriSource = new Uri(dialog.FileName);
                image.EndInit();
                CharacterImage.Source = image;
                txtImage.Text = dialog.FileName;
            }
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (
                txtCharacterNo.Text.Trim() == "" ||
                txtAlias.Text.Trim() == "" ||
                cmbUniverse.SelectedIndex == -1 ||
                cmbTeam.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill in all the required fields.");
            }
            else 
            {
                if (model != null && model.Id != 0)
                {
                    Character character = database.Characters.Find(model.Id);
                    List<Character> characterList = database.Characters.Where(characterItem =>
                    characterItem.CharacterNo == Convert.ToInt32(txtCharacterNo.Text) && characterItem.Id != character.Id).ToList();
                    if (characterList.Count > 0)
                    {
                        MessageBox.Show("This Character Number is already taken. Please try a diffrerent one.");
                    }
                    else
                    {
                        if (txtImage.Text.Trim() != "")
                        {
                            if (File.Exists(@"Images//" + character.ImagePath))
                            {
                                File.Delete(@"Images//" + character.ImagePath);
                                string filename = "";
                                string Unique = Guid.NewGuid().ToString();
                                filename += Unique + Path.GetFileName(txtImage.Text);
                                File.Copy(txtImage.Text, @"Images//" + filename);
                                character.ImagePath = filename;
                            }
                        }
                        character.CharacterNo = Convert.ToInt32(txtCharacterNo.Text);
                        TextRange Bio = new TextRange(txtBio.Document.ContentStart, txtBio.Document.ContentEnd);
                        character.Bio = Bio.Text;
                        character.UniverseId = Convert.ToInt32(cmbUniverse.SelectedValue);
                        character.TeamId = Convert.ToInt32(cmbTeam.SelectedValue);
                        character.Alias = txtAlias.Text;
                        character.Name = txtName.Text;
                        character.Surname = txtSurname.Text;
                        database.SaveChanges();
                        MessageBox.Show("Character was updated successfully.");
                    }
                }
                else
                {
                    var UniqueCharacterNumbers = database.Characters.Where(character => character.CharacterNo == Convert.ToInt32(txtCharacterNo.Text)).ToList();

                    if (UniqueCharacterNumbers.Count > 0)
                    {
                        MessageBox.Show("This Character Number is already taken. Please try a diffrerent one.");
                    }
                    else
                    {
                        Character character = new();
                        character.CharacterNo = Convert.ToInt32(txtCharacterNo.Text);
                        character.Alias = txtAlias.Text;
                        character.Name = txtName.Text;
                        character.Surname = txtSurname.Text;
                        character.UniverseId = Convert.ToInt32(cmbUniverse.SelectedValue);
                        character.TeamId = Convert.ToInt32(cmbTeam.SelectedValue);
                        TextRange text = new(txtBio.Document.ContentStart, txtBio.Document.ContentEnd);
                        character.Bio = text.Text;
                        string filename = "";
                        string Unique = Guid.NewGuid().ToString();
                        filename += Unique + dialog.SafeFileName;
                        character.ImagePath = filename;
                        database.Characters.Add(character);
                        database.SaveChanges();
                        if (txtImage.Text.Length > 0)
                        {
                            File.Copy(txtImage.Text, @"Images//" + filename);
                        }
                        MessageBox.Show("New Character was added successfully.");
                        txtCharacterNo.Clear();
                        txtAlias.Clear();
                        txtName.Clear();
                        txtSurname.Clear();
                        cmbUniverse.SelectedIndex = -1;
                        cmbTeam.ItemsSource = teams;
                        cmbTeam.SelectedIndex = -1;
                        txtBio.Document.Blocks.Clear();
                        CharacterImage.Source = new BitmapImage();
                        txtImage.Clear();
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
