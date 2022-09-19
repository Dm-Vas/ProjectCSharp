using Microsoft.EntityFrameworkCore;
using SuperheroLog.Database;
using SuperheroLog.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        void FillDataGrid()
        {
            missionList = database.Missions
                   .Include(mission => mission.MissionStatusNavigation)
                   .Include(mission => mission.Character)
                   .ThenInclude(mission => mission.Universe)
                   .ThenInclude(mission => mission.Teams)
                   .Select(mission => new MissionDetailModel()
                   {
                       Id = mission.Id,
                       CharacterId = (int)mission.CharacterId,
                       StatusName = mission.MissionStatusNavigation.StatusName,
                       MissionDescription = mission.MissionDescription,
                       MissionStatus = (int)mission.MissionStatus,
                       MissionName = mission.MissionName,
                       CharacterNo = mission.Character.CharacterNo,
                       Alias = mission.Character.Alias,
                       UniverseId = mission.Character.UniverseId,
                       TeamId = mission.Character.TeamId
                   }
                   ).ToList();

            gridMission.ItemsSource = missionList;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            MissionWindow window = new();
            window.ShowDialog();
            FillDataGrid();
        }

        SuperheroDataContext database = new();
        List<MissionDetailModel> missionList = new();
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }

        MissionDetailModel model = new();
        private void GridMission_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            model = (MissionDetailModel)gridMission.SelectedItem;
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (model != null && model.Id != 0)
            {
                MissionWindow window = new();
                window.model = model;
                window.ShowDialog();
                FillDataGrid();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (model != null && model.Id != 0)
            {
                if (MessageBox.Show("Are you sure you want to delete this mission?",
                "Question",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning)
                == MessageBoxResult.Yes
                )
                {
                    if (model.Id != 0)
                    {
                        MissionDetailModel missionModel = (MissionDetailModel)gridMission.SelectedItem;
                        Mission mission = database.Missions.First(mission => mission.Id == missionModel.Id);

                        database.Missions.Remove(mission);
                        database.SaveChanges();

                        MessageBox.Show("Mission was deleted successfully.");
                        FillDataGrid();
                    }
                }
            }
        }
    }
}
