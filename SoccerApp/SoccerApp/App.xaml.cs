using SoccerApp.Core.Helpers;
using SoccerApp.Helpers;
using SoccerApp.Models;
using SoccerApp.Views;
using SoccerApp.Services;
using SoccerApp.ViewModels;
using SoccerApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SoccerApp
{
    public partial class App : Application
    {
        #region Attributes
        private DataService dataService;
        private ApiService apiService;
        private DialogService dialogService;
        private NavigationService navigationService;
        #endregion
        public static App Instance;
        #region Properties
        public static NavigationPage Navigator { get; internal set; }

        public static MasterPage Master { get; internal set; }
        #endregion

        public  Action HideLoginView
        {
            get
            {
                return new Action(() => App.Current.MainPage = new LoginPage());
            }
        }

        public async void NavigateToProfile(FacebookResponse profile)
        {
            var parameter = dataService.First<Parameter>(false);
            var token = await apiService.LoginFacebook(parameter.URLBase, "/api", "/Users/LoginFacebook", profile);

            if (token == null)
            {
                App.Current.MainPage = new LoginPage();
                return;
            }

            var response = await apiService.GetUserByEmail(parameter.URLBase, "/api", "/Users/GetUserByEmail", token.TokenType, token.AccessToken, token.UserName);

            if (response == null)
            {
                await dialogService.ShowMessage("Error", "The user name or password in incorrect.");
                return;
            }


            var user = (User)response.Result;
            user.AccessToken = token.AccessToken;
            user.TokenType = token.TokenType;
            user.TokenExpires = token.Expires;
            user.IsRemembered = true;
            user.Password = profile.Id;
            dataService.DeleteAllAndInsert(user.FavoriteTeam);
            dataService.DeleteAllAndInsert(user.UserType);
            dataService.DeleteAllAndInsert(user);

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.CurrentUser = user;
            
            navigationService.SetMainPage("MasterPage");
        }


        #region Constructors
        public App()
        {
            Instance = this;
            InitializeComponent();
            dialogService = new DialogService();
            apiService = new ApiService();
            dataService = new DataService();
            LoadParameters();
            navigationService = new NavigationService();
            var user = dataService.First<User>(false);
            if (user == null)
            {
                MainPage = new LoginPage();
                return;
            }


            var favoriteTeam = dataService.Find<Team>(user.FavoriteTeamId, false);
            user.FavoriteTeam = favoriteTeam;
            if (user != null && user.IsRemembered && user.TokenExpires > DateTime.Now)
            {
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.SetCurrentUser(user);
                MainPage = new MasterPage();
            }
            else
            {
                MainPage = new LoginPage();
            }
        }
        #endregion
        protected override void OnStart()
        {
            AppCenter.Start("android=6757ecd0-45e0-44dc-9280-c95152f3721b;" +
                              "uwp={Your UWP App secret here};" +
                              "ios={Your iOS App secret here}",
                              typeof(Analytics), typeof(Crashes));
        }
        #region Methods
        private void LoadParameters()
        {
            var urlBase = Application.Current.Resources["URLBase"].ToString();
            var urlBase2 = Application.Current.Resources["URLBase2"].ToString();

            var parameter = dataService.First<Parameter>(false);
            if (parameter == null)
            {
                parameter = new Parameter
                {
                    URLBase = urlBase,
                    URLBase2 = urlBase2,
                };

                dataService.Insert(parameter);
            }
            else
            {
                parameter.URLBase = urlBase;
                parameter.URLBase2 = urlBase2;
                dataService.Update(parameter);
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        #endregion
    }

}
