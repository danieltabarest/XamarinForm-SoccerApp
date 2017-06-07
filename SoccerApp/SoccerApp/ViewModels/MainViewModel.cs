using System;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Diagnostics;
using System.Collections.Generic;
using SoccerApp.ViewModels;
using System.ComponentModel;
using SoccerApp.Models;
using GalaSoft.MvvmLight.Command;
using Plugin.Connectivity;
using SoccerApp.Services;
using SoccerApp.ViewModels.Soccer.ViewModels;

namespace SoccerApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Attributes
        private User currentUser;
        private ApiService apiService;
        private DataService dataService;
        private DialogService dialogService;
        private NavigationService navigationService;
        private bool isRefreshing = false;
        //private User currentUser;
        #endregion

        #region Properties
        public LoginViewModel Login { get; set; }

        public SelectTournamentViewModel SelectTournament { get; set; }
        public ConfigViewModel Config { get; set; }

        public SelectUserGroupViewModel SelectUserGroup { get; set; }

        public UserGroupViewModel UserGroup { get; set; }

        public PositionsViewModel Position { get; set; }
        public NewUserViewModel NewUser{ get; set; }

        public MyResultViewModel MyResult { get; set; }

        
        public SelectGroupViewModel SelectGroup { get; set; }

        public ChangePasswordViewModel ChangePassword { get; set; }

        public SelectMatchViewModel SelectMatch { get; set; }
        
        public EditPreditionsViewModel EditPreditions { get; set; }

        public ObservableCollection<MenuItemViewModel> Menu { get; set; }

        public User CurrentUser
        {
            set
            {
                if (currentUser != value)
                {
                    currentUser = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentUser"));
                }
            }
            get
            {
                return currentUser;
            }
        }
        #endregion

        #region Constructor
        public MainViewModel()
        {
            instance = this;
            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            dataService = new DataService();
            Login = new LoginViewModel();

            LoadMenu();
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }

            return instance;
        }
        #endregion

        #region Methods
        private void LoadMenu()
        {
            Menu = new ObservableCollection<MenuItemViewModel>();

            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_assignment_black_24dp.png",
                PageName = "SelectTournamentPage",
                Title = "Predictions",
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_book_black_24dp.png",
                PageName = "SelectUserGroupsPage",
                Title = "Groups",
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_card_membership_black_24dp.png",
                PageName = "SelectTournamentPage",
                Title = "Tournaments",
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_format_list_numbered_black_24dp.png",
                PageName = "SelectTournamentPage",
                Title = "My Results",
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_group_work_black_24dp.png",
                PageName = "ConfigPage",
                Title = "Config",
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_close_black_24dp.png",
                PageName = "LoginPage",
                Title = "Logout",
            });
        }

        public void SetCurrentUser(User user)
        {
            CurrentUser = user;
        }
        #endregion

        #region Methods
        private async void RefreshPoints()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return;
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!isReachable)
            {
                return;
            }

            isRefreshing = true;
            var parameters = dataService.First<Parameter>(false);
            var user = dataService.First<User>(false);

            var response = await apiService.GetPoints<Models.Point>(parameters.URLBase, "/api", "Users/GetPoints", user.TokenType, user.AccessToken, user.UserId);

            if (!response.IsSuccess)
            {
                return;
            }

            var point = (Models.Point)response.Result;
            if (CurrentUser.Points != point.Points) 
            {
                CurrentUser.Points = point.Points;
                dataService.Update(CurrentUser);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentUser"));
            }

        }


        #endregion

        #region Commands
        public ICommand RefreshPointsCommand { get { return new RelayCommand(RefreshPoints); } }

        

        public void Refresh()
        {
            //IsRefreshing = true;
            //LoadTournaments();
            //IsRefreshing = false;
        }

        internal void RegisterDevice()
        {
            throw new NotImplementedException();
        }
        #endregion
    }

}