using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

using GalaSoft.MvvmLight.Command;
using Plugin.Connectivity;

using SoccerApp.Services;
using SoccerApp.Views;
using SoccerApp;
using SoccerApp.Models;
using SoccerApp.ViewModels;

namespace SoccerApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Attributes
        private ApiService apiService;

        private DialogService dialogService;

        private DataService dataService;

        private NavigationService navigationService;

        private string email;

        private string password;

        private bool isRunning;

        private bool isEnabled;

        private bool isRemembered;
        #endregion

        #region Properties
        public string Email
        {
            set
            {
                if (email != value)
                {
                    email = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Email"));
                }
            }
            get
            {
                return email;
            }
        }

        public string Password
        {
            set
            {
                if (password != value)
                {
                    password = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Password"));
                }
            }
            get
            {
                return password;
            }
        }

        public bool IsRunning
        {
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
                }
            }
            get
            {
                return isRunning;
            }
        }

        public bool IsEnabled
        {
            set
            {
                if (isEnabled != value)
                {
                    isEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsEnabled"));
                }
            }
            get
            {
                return isEnabled;
            }
        }

        public bool IsRemembered
        {
            set
            {
                if (isRemembered != value)
                {
                    isRemembered = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRemembered"));
                }
            }
            get
            {
                return isRemembered;
            }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            apiService = new ApiService();
            dialogService = new DialogService();
            dataService = new DataService();
            navigationService = new NavigationService();

            IsRemembered = true;
            IsEnabled = true;
            Email = null;
            Password = null;
        }
        #endregion

        #region Commands
        public ICommand LoginCommand { get { return new RelayCommand(Login); } }

        public ICommand RegisterCommand { get { return new RelayCommand(Register); } }

        public ICommand LoginFacebookCommand { get { return new RelayCommand(LoginFacebook); } }


        private void Register()
        {
            navigationService.SetMainPage("NewUserPage");
        }

        private void LoginFacebook()
        {
            try
            {
                //this.Navigation.PushAsync((Page)Activator.CreateInstance(item.TargetType));
                App.Current.MainPage = new LoginFacebookPage();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async void Login()
        {
            try
            {
                if (string.IsNullOrEmpty(Email))
                {
                    await dialogService.ShowMessage("Error", "You must enter the user email.");
                    return;
                }

                if (string.IsNullOrEmpty(Password))
                {
                    await dialogService.ShowMessage("Error", "You must enter a password.");
                    return;
                }

                IsRunning = true;
                IsEnabled = false;

                if (!CrossConnectivity.Current.IsConnected)
                {
                    IsRunning = false;
                    IsEnabled = true;
                    await dialogService.ShowMessage("Error", "Check you internet connection.");
                    return;
                }

                var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
                if (!isReachable)
                {
                    IsRunning = false;
                    IsEnabled = true;
                    await dialogService.ShowMessage("Error", "Check you internet connection.");
                    return;
                }

                var parameters = dataService.First<Parameter>(false);
                var token = await apiService.GetToken(parameters.URLBase, Email, Password);

                if (token == null)
                {
                    IsRunning = false;
                    IsEnabled = true;
                    await dialogService.ShowMessage("Error", "The user name or password in incorrect.");
                    Password = null;
                    return;
                }

                if (string.IsNullOrEmpty(token.AccessToken))
                {
                    IsRunning = false;
                    IsEnabled = true;
                    await dialogService.ShowMessage("Error", token.ErrorDescription);
                    Password = null;
                    return;
                }

                var response = await apiService.GetUserByEmail(parameters.URLBase, "/api", "/Users/GetUserByEmail", token.TokenType, token.AccessToken, token.UserName);

                if (!response.IsSuccess)
                {
                    IsRunning = false;
                    IsEnabled = true;
                    await dialogService.ShowMessage("Error", "Problem ocurred retrieving user information, try latter.");
                    return;
                }

                var user = (User)response.Result;
                user.AccessToken = token.AccessToken;
                user.TokenType = token.TokenType;
                user.TokenExpires = token.Expires;
                user.IsRemembered = IsRemembered;
                user.Password = Password;
                dataService.DeleteAllAndInsert(user.FavoriteTeam);
                dataService.DeleteAllAndInsert(user.UserType);
                dataService.DeleteAllAndInsert(user);

                var mainViewModel = MainViewModel.GetInstance();
                //mainViewModel.SetCurrentUser(new User());//user);
                mainViewModel.CurrentUser = user;
                //mainViewModel.RegisterDevice();
                navigationService.SetMainPage("MasterPage");

                Email = null;
                Password = null;
                IsRunning = false;
                IsEnabled = true;
            }
            catch (Exception ex)
            {
                await dialogService.ShowMessage("Error", "Problem ocurred retrieving user information, try latter." + ex.Message);
            }
        }
        #endregion

    }
}
