﻿using SoccerApp.ViewModels;
using SoccerApp.Models;
using SoccerApp.Views;
using System.Threading.Tasks;

namespace SoccerApp.Services
{
    public class NavigationService
    {
        #region Attributes
        private DataService dataService;
        #endregion

        #region Constructors
        public NavigationService()
        {
            dataService = new DataService();
        }
        #endregion

        #region Methods

        public async Task Navigate(string pageName)
        {
            App.Master.IsPresented = false;
            var mainViewModel = MainViewModel.GetInstance();

            switch (pageName)
            {

                case "SelectGroupPage":
                    await App.Navigator.PushAsync(new SelectGroupPage());
                    break;
                case "SelectTournamentPage":
                    //mainViewModel.SelectTournament = new SelectTournamentViewModel();
                    await App.Navigator.PushAsync(new SelectTournamentPage());
                    break;
                case "SelectMatchPage":
                    //mainViewModel.SelectTournament = new SelectTournamentViewModel();
                    await App.Navigator.PushAsync(new SelectMatchPage());
                    break;
                case "EditPreditionsPage":
                    //mainViewModel.SelectTournament = new SelectTournamentViewModel();
                    await App.Navigator.PushAsync(new EditPredictionPage());
                    break;
                case "ConfigPage":
                    await App.Navigator.PushAsync(new ConfigPage());
                    break;
                case "PositionPage":
                    await App.Navigator.PushAsync(new PositionPage());
                    break;
                case "ChangePassword":
                    await App.Navigator.PushAsync(new ChangePasswordPage());
                    break;
                case "SelectUserGroupsPage":
                    await App.Navigator.PushAsync(new SelectUserGroupPage());
                    break;

                case "TournamentsPage":
                    await App.Navigator.PushAsync(new SelectTournamentPage());
                    break;

                case "UserGroupPage":
                    await App.Navigator.PushAsync(new UserGroupPage());
                    break;
                case "MyResultsPage":
                    await App.Navigator.PushAsync(new MyResultsPage());
                    break;
                    
                default:
                    break;
            }
        }

        public void SetMainPage(string pageName)
        {
            switch (pageName)
            {
                case "MasterPage":
                    App.Current.MainPage = new MasterPage();
                    break;
                case "LoginPage":
                    Logout();
                    App.Current.MainPage = new LoginPage();
                    break;
                case "NewUserPage":
                    Logout();
                    App.Current.MainPage = new NewUserPage();
                    break;
                default:
                    break;
            }
        }

        private void Logout()
        {
            var user = dataService.First<User>(false);
            if (user != null)
            {
                user.IsRemembered = false;
                dataService.Update(user);
            }
        }

        public async Task Back()
        {
            await App.Navigator.PopAsync();
        }

        public async Task Clear()
        {
            await App.Navigator.PopToRootAsync();
        }
        #endregion
    }
}
