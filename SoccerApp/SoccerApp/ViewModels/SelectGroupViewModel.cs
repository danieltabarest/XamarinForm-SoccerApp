using GalaSoft.MvvmLight.Command;
using Plugin.Connectivity;
using SoccerApp.Models;
using SoccerApp.Services;
using SoccerApp.ViewModels.Soccer.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace SoccerApp.ViewModels
{
    public class SelectGroupViewModel
    {
        #region Attributes
        private ApiService apiService;
        private DataService dataService;
        private DialogService dialogService;
        private NavigationService navigationService;
        private List<Group> groups;
        #endregion

        #region Properties
        public ObservableCollection<GroupItemViewModel> Groups { get; set; }
        #endregion

        #region Constructor
        public SelectGroupViewModel(List<Group> groups)
        {
            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            dataService = new DataService();

            this.groups = groups;
            Groups = new ObservableCollection<GroupItemViewModel>();
            LoadGroups();
        }
        #endregion

        #region Methods
        private void LoadGroups()
        {
            Groups.Clear();
            foreach (var group in groups)
            {
                Groups.Add(new GroupItemViewModel
                {
                    Name = group.Name,
                    TournamentGroupId = group.TournamentGroupId,
                    TournamentId = group.TournamentId,
                });
            }
        }
        #endregion
    }


}
