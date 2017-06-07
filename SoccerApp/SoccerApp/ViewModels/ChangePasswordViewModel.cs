using GalaSoft.MvvmLight.Command;
using Plugin.Connectivity;
using SoccerApp.Helpers;
using SoccerApp.Models;
using SoccerApp.Services;
using SoccerApp.ViewModels.Soccer.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;


namespace SoccerApp.ViewModels
{
    public class ChangePasswordViewModel : INotifyPropertyChanged
    {
        #region Attributes
        private ApiService apiService;
        private DataService dataService;
        private DialogService dialogService;
        private NavigationService navigationService;
        private bool isRefreshing = false;
        private int tournamentId;
        private bool isRunning;
        private bool isEnabled;
        private string currentPassword;
        private string newPassword;
        private string confirmPassword;

        #endregion

        #region Properties
        public ObservableCollection<MatchItemViewModel> Matches { get; set; }

        public string CurrentPassword
        {
            set
            {
                if (currentPassword != value)
                {
                    currentPassword = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentPassword"));
                }
            }
            get
            {
                return currentPassword;
            }
        }
        public string NewPassword
        {
            set
            {
                if (newPassword != value)
                {
                    newPassword = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NewPassword"));
                }
            }
            get
            {
                return newPassword;
            }
        }
        public string ConfirmPassword
        {
            set
            {
                if (confirmPassword != value)
                {
                    confirmPassword = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ConfirmPassword"));
                }
            }
            get
            {
                return confirmPassword;
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
        public ChangePasswordViewModel()
        {
            this.tournamentId = 1;
           // instance = this;

            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            dataService = new DataService();

            Matches = new ObservableCollection<MatchItemViewModel>();
        }
        #endregion

        #region Singleton
        private static SelectMatchViewModel instance;

        public static SelectMatchViewModel GetInstance()
        {
            return instance;
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Methods
        private async void LoadMatches()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await dialogService.ShowMessage("Error", "Check you internet connection.");
                await navigationService.Clear();
                return;
            }

            var parameter = dataService.First<Parameter>(false);
            var user = dataService.First<User>(false);
            var response = await apiService.Get<Tournament>(parameter.URLBase, "/api", "/Tournaments", user.TokenType, user.AccessToken);
            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);
                await navigationService.Clear();
                return;
            }

            ReloadTournaments((List<Tournament>)response.Result);
        }

        private void ReloadTournaments(List<Tournament> tournaments)
        {
            Matches.Clear();
            foreach (var tournament in tournaments)
            {
                Matches.Add(new MatchItemViewModel
                {
                    //Dates = tournament.Dates,
                    //Groups = tournament.Groups,
                    //Logo = tournament.Logo,
                    //Name = tournament.Name,
                    //TournamentId = tournament.TournamentId,
                });
            }
        }
        #endregion

        #region Commands

        public ICommand ChangePasswordCommand { get { return new RelayCommand(ChangePassword); } }

        public async void ChangePassword()
        {
            if (string.IsNullOrEmpty(CurrentPassword))
            {
                await dialogService.ShowMessage("Error", "You must enter a password.");
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();
            if (mainViewModel.CurrentUser.Password !=CurrentPassword)
            {
                await dialogService.ShowMessage("Error", "the current password does not match");
                return;
            }

            if (string.IsNullOrEmpty(NewPassword))
            {
                await dialogService.ShowMessage("Error", "You must enter a new password.");
                return;


            }

            if (NewPassword == CurrentPassword)
            {
                await dialogService.ShowMessage("Error", "The password is equal to new password");
                return;
            }

            if (NewPassword.Length < 6)
            {
                await dialogService.ShowMessage("Error", "The password must have al least 6 characters");
                return;
            }



            if (CurrentPassword.Length < 6)
            {
                await dialogService.ShowMessage("Error", "The password must have al least 6 characters");
                return;
            }

            if (string.IsNullOrEmpty(ConfirmPassword))
            {
                await dialogService.ShowMessage("Error", "You must enter a password confirm.");
                return;
            }

            if (NewPassword != ConfirmPassword)
            {
                await dialogService.ShowMessage("Error", "The password and confirm does not match.");
                return;
            }


            IsRunning = true;
            IsEnabled = false;

            var parameters = dataService.First<Parameter>(false);
            var user = dataService.First<User>(false);

            ChangePasswordRequest request = new ChangePasswordRequest
            {
                CurrentPassword = CurrentPassword,
                Email = user.Email,
                NewPassword = NewPassword,

            };
            var response = await apiService.ChangePassword(parameters.URLBase, "/api", "/Users/ChangePassword", user.TokenType, user.AccessToken,
                request);

            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            await dialogService.ShowMessage("Confirmation", "The password was changed");
            await navigationService.Back();
        }

        public ICommand RefreshCommand { get { return new RelayCommand(Refresh); } }

        public void Refresh()
        {
            IsRefreshing = true;
            LoadMatches();
            IsRefreshing = false;
        }
        #endregion
    }


}
