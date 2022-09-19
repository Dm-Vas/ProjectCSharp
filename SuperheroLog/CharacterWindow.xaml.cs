using Microsoft.Win32;
using SuperheroLog.Database;
using SuperheroLog.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
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
        SUPERHEROBASEContext database = new();
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

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CmbUniverse_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
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

        private void TxtCharacterNo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (
                txtCharacterNo.Text.Trim() == "" ||
                txtAlias.Text.Trim() == "" ||
                txtImage.Text.Trim() == "" ||
                cmbUniverse.SelectedIndex == -1 ||
                cmbTeam.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill in all the required fields");
            }
            else {

                if (model != null && model.Id != 0) 
                {
                    Character character = database.Characters.Find(model.Id);
                    List<Character> characterList = database.Characters.Where(characterItem =>
                      characterItem.CharacterNo == 
                      Convert.ToInt32(txtCharacterNo.Text) &&
                      characterItem.Id != character.Id).ToList();

                    if (characterList.Count > 0)
                    {
                        MessageBox.Show("This Character Number already exists. Please try another one");
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
                                filename += Unique + System.IO.Path.GetFileName(txtImage.Text);
                                File.Copy(txtImage.Text, @"Images//" + filename);
                                character.ImagePath = filename;
                            }


                        }
                        character.CharacterNo = Convert.ToInt32(txtCharacterNo.Text);
                        character.Alias = txtAlias.Text;
                        character.Name = txtName.Text;
                        character.Surname = txtSurname.Text;
                        character.UniverseId = Convert.ToInt32(cmbUniverse.SelectedValue);
                        character.TeamId = Convert.ToInt32(cmbTeam.SelectedValue);
                        TextRange Bio = new(txtBio.Document.ContentStart, txtBio.Document.ContentEnd);
                        character.Bio = Bio.Text;
                        database.SaveChanges();
                        MessageBox.Show("Character was updated successfully");
                    }
                }
                else
                {
                    var Uniquelist = database.Characters.Where(character => character.CharacterNo == Convert.ToInt32(txtCharacterNo.Text)).ToList();

                    if (Uniquelist.Count > 0)
                    {
                        MessageBox.Show("This Character Number is already taken. Please try another one");
                    }
                    else
                    {
                        Character character = new()
                        {
                            CharacterNo = Convert.ToInt32(txtCharacterNo.Text),
                            Alias = txtAlias.Text,
                            Name = txtName.Text,
                            Surname = txtSurname.Text,
                        };
                        TextRange bio = new(txtBio.Document.ContentStart, txtBio.Document.ContentEnd);
                        character.Bio = bio.Text;
                        character.UniverseId = Convert.ToInt32(cmbUniverse.SelectedValue);
                        character.TeamId = Convert.ToInt32(cmbTeam.SelectedValue);

                        string filename = "";
                        string Unique = Guid.NewGuid().ToString();
                        filename += Unique + dialog.SafeFileName;
                        character.ImagePath = filename;
                        database.Characters.Add(character);
                        database.SaveChanges();

                        File.Copy(txtImage.Text, @"Images//" + filename);
                        MessageBox.Show("New Character was added successfully");

                        txtCharacterNo.Clear();
                        txtAlias.Clear();
                        txtName.Clear();
                        txtSurname.Clear();
                        txtBio.Document.Blocks.Clear();
                        txtImage.Clear();
                        cmbUniverse.SelectedIndex = -1;
                        cmbTeam.ItemsSource = teams;
                        cmbTeam.SelectedIndex = -1;
                        CharacterImage.Source = new BitmapImage();
                    }
                }
            }
        }
    }
}
