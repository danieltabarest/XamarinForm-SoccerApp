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
    public class SelectUserGroupViewModel :UserGroup, INotifyPropertyChanged
    {
        #region Attributes
        private ApiService apiService;
        private DataService dataService;
        private DialogService dialogService;
        private NavigationService navigationService;
        private bool isRefreshing = false;
        private int tournamentId;
        #endregion

        #region Properties
        public ObservableCollection<UserGroupItemViewModel> UserGroups { get; set; }

        public bool IsRefreshing
        {
            set
            {
                if (isRefreshing != value)
                {
                    isRefreshing = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("IsRefreshing"));
                    }
                }
            }
            get
            {
                return isRefreshing;
            }
        }
        #endregion

        #region Constructor
        public SelectUserGroupViewModel()
        {
            this.tournamentId = tournamentId;
            // instance = this;

            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            dataService = new DataService();

            UserGroups = new ObservableCollection<UserGroupItemViewModel>();

            LoadUserGroups();
        }
        #endregion

        #region Singleton
        private static SelectUserGroupViewModel instance;

        public static SelectUserGroupViewModel GetInstance()
        {
            return instance;
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Methods
        private async void LoadUserGroups()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await dialogService.ShowMessage("Error", "Check you internet connection.");
                await navigationService.Clear();
                return;
            }
            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!isReachable)
            {

                await dialogService.ShowMessage("Error", "Check you internet connection.");
                return;
            }

            isRefreshing = true;
            var parameters = dataService.First<Parameter>(false);
            var user = dataService.First<User>(false);

            var response = await apiService.Get<UserGroup>(parameters.URLBase, "/api", "/Groups", user.TokenType, user.AccessToken, user.UserId);
            isRefreshing = false;

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            ReloadUserGroups((List<UserGroup>)response.Result);
        }

        private void ReloadUserGroups(List<UserGroup> userGroups)
        {
            UserGroups.Clear();
            foreach (var userg in userGroups)
            {
                UserGroups.Add(new UserGroupItemViewModel
                {
                    GroupUsers = userg.GroupUsers,
                    Logo = userg.Logo,
                    Owner = userg.Owner,
                    OwnerId = userg.OwnerId,
                    UserGroupId = userg.UserGroupId,
                    Name = userg.Name,
                });
            }
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand { get { return new RelayCommand(Refresh); } }

        public void Refresh()
        {
            IsRefreshing = true;
            LoadUserGroups();
            IsRefreshing = false;
        }
        #endregion
    }


}
