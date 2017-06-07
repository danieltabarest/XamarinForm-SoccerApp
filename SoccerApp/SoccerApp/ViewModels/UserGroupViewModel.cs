using GalaSoft.MvvmLight.Command;
using Plugin.Connectivity;
using SoccerApp.Models;
using SoccerApp.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;


namespace SoccerApp.ViewModels
{

    namespace Soccer.ViewModels
    {

        public class UserGroupViewModel : UserGroup
        {

            private NavigationService navigationService;
            private UserGroup usergrou;

            #region Attributes
            private ApiService apiService;
            private DataService dataService;
            private DialogService dialogService;
            #endregion

            #region Properties
            public ObservableCollection<GroupUserItemViewModel> MyGroupsUsers { get; set; }
            #endregion

            public UserGroupViewModel(UserGroup usergrou)
            {
                //navigationService = new NavigationService();
                this.usergrou = usergrou;

                UserGroupId = usergrou.UserGroupId;
                Name = usergrou.Name;
                Logo = usergrou.Logo;
                Owner = usergrou.Owner;
                GroupUsers = usergrou.GroupUsers;


                MyGroupsUsers = new ObservableCollection<GroupUserItemViewModel>();

                //LoadUserGroups();
                ReloadGroupsUser(GroupUsers);
            }

            #region Singleton
            private static UserGroupViewModel instance;

            public static UserGroupViewModel GetInstance()
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

                var parameters = dataService.First<Parameter>(false);
                var user = dataService.First<User>(false);

                var response = await apiService.Get<UserGroup>(parameters.URLBase, "/api", "/Groups", user.TokenType, user.AccessToken);

                if (!response.IsSuccess)
                {
                    await dialogService.ShowMessage("Error", response.Message);
                    return;
                }

                ReloadGroupsUser((List<GroupUser>)response.Result);
            }

            private void ReloadGroupsUser(List<GroupUser> groupsuser)
            {
                try
                {
                    MyGroupsUsers.Clear();
                    foreach (var userg in groupsuser)
                    {
                        MyGroupsUsers.Add(new GroupUserItemViewModel
                        {
                            GroupId = userg.GroupId,
                            Group = userg.Group,
                            IsBlocked = userg.IsBlocked,
                            GroupUserId = userg.GroupUserId,
                            IsAccepted = userg.IsAccepted,
                            Points = userg.Points,
                            User = userg.User,
                            UserId = userg.UserId
                        });
                    }
                }
                catch (System.Exception ex)
                {

                }
            }
            #endregion

            #region Commands
            public ICommand RefreshCommand { get { return new RelayCommand(Refresh); } }

            public void Refresh()
            {
                LoadUserGroups();
            }
            #endregion
        }


    }

}
