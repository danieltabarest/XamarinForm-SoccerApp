﻿using GalaSoft.MvvmLight.Command;
using Plugin.Connectivity;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using SoccerApp.Models;
using SoccerApp.Services;
using SoccerApp.ViewModels.Soccer.ViewModels;
using SoccerApp.Helpers;

namespace SoccerApp.ViewModels
{
    public class NewUserViewModel : User, INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Attributes
        private ApiService apiService;
        private DialogService dialogService;
        private NavigationService navigationService;
        private DataService dataService;
        private bool isRunning;
        private bool isEnabled;
        private int favoriteLeagueId;
        private List<League> leagues;
        private ImageSource imageSource;
        private MediaFile file;
        #endregion

        #region Properties
        public string PasswordConfirm { get; set; }

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

        public int FavoriteLeagueId
        {
            set
            {
                if (favoriteLeagueId != value)
                {
                    favoriteLeagueId = value;
                    ReloadTeams(favoriteLeagueId);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FavoriteLeagueId"));
                }
            }
            get
            {
                return favoriteLeagueId;
            }
        }

        public ImageSource ImageSource
        {
            set
            {
                if (imageSource != value)
                {
                    imageSource = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ImageSource"));
                }
            }
            get
            {
                return imageSource;
            }
        }

        public ObservableCollection<LeagueItemViewModel> Leagues { get; set; }

        public ObservableCollection<TeamItemViewModel> Teams { get; set; }
        #endregion

        #region Constructor
        public NewUserViewModel()
        {
            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            dataService = new DataService();

            Leagues = new ObservableCollection<LeagueItemViewModel>();
            Teams = new ObservableCollection<TeamItemViewModel>();

            Picture = "avatar_user.png";
            IsEnabled = true;

            LoadLeagues();
        }
        #endregion

        #region Methods
        private async void LoadLeagues()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await dialogService.ShowMessage("Error", "Check you internet connection.");
                return;
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!isReachable)
            {
                await dialogService.ShowMessage("Error", "Check you internet connection.");
                return;
            }

            IsRunning = true;
            var parameters = dataService.First<Parameter>(false);
            var response = await apiService.Get<League>(parameters.URLBase, "/api", "/Leagues");
            IsRunning = false;

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            leagues = (List<League>)response.Result;
            ReloadLeagues(leagues);
        }

        private void ReloadLeagues(List<League> leagues)
        {
            Leagues.Clear();
            foreach (var league in leagues.OrderBy(l => l.Name))
            {
                Leagues.Add(new LeagueItemViewModel
                {
                    LeagueId = league.LeagueId,
                    Logo = league.Logo,
                    Name = league.Name,
                    Teams = league.Teams,
                });
            }
        }

        private void ReloadTeams(int favoriteLeagueId)
        {
            var teams = leagues.Where(l => l.LeagueId == favoriteLeagueId).FirstOrDefault().Teams;
            Teams.Clear();
            foreach (var team in teams.OrderBy(t => t.Name))
            {
                Teams.Add(new TeamItemViewModel
                {
                    Fans = team.Fans,
                    Initials = team.Initials,
                    LeagueId = team.LeagueId,
                    Logo = team.Logo,
                    Name = team.Name,
                    TeamId = team.TeamId,
                });
            }
        }
        #endregion

        #region Commands
        public ICommand SaveCommand { get { return new RelayCommand(Save); } }

        private async void Save()
        {
            if (string.IsNullOrEmpty(FirstName))
            {
                await dialogService.ShowMessage("Error", "You must enter a first name.");
                return;
            }

            if (string.IsNullOrEmpty(LastName))
            {
                await dialogService.ShowMessage("Error", "You must enter a last name.");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await dialogService.ShowMessage("Error", "You must enter a password.");
                return;
            }

            if (Password.Length < 6)
            {
                await dialogService.ShowMessage("Error", "The password must have at least 6 characters.");
                return;
            }

            if (string.IsNullOrEmpty(PasswordConfirm))
            {
                await dialogService.ShowMessage("Error", "You must enter a password confirm.");
                return;
            }

            if (Password != PasswordConfirm)
            {
                await dialogService.ShowMessage("Error", "The password and confirm does not match.");
                return;
            }

            if (string.IsNullOrEmpty(Email))
            {
                await dialogService.ShowMessage("Error", "You must enter a email.");
                return;
            }

            if (string.IsNullOrEmpty(NickName))
            {
                await dialogService.ShowMessage("Error", "You must enter a nick name.");
                return;
            }

            if (FavoriteTeamId == 0)
            {
                await dialogService.ShowMessage("Error", "You must select a favorite team.");
                return;
            }

            if (!CrossConnectivity.Current.IsConnected)
            {
                await dialogService.ShowMessage("Error", "Check you internet connection.");
                return;
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!isReachable)
            {
                await dialogService.ShowMessage("Error", "Check you internet connection.");
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var imageArray = FilesHelper.ReadFully(file.GetStream());
            file.Dispose();

            var user = new User
            {
                Email = Email,
                FavoriteTeamId = FavoriteTeamId,
                FirstName = FirstName,
                ImageArray = imageArray,
                LastName = LastName,
                NickName = NickName,
                Password = Password,
                UserTypeId = 1,
            };

            var parameters = dataService.First<Parameter>(false);
            var response = await apiService.Post(parameters.URLBase, "/api", "/Users", user);

            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            await dialogService.ShowMessage("Confirmation", "The user was created, please login.");
            await navigationService.Back();
        }

        public ICommand CancelCommand { get { return new RelayCommand(Cancel); } }

        private void Cancel()
        {
            navigationService.SetMainPage("LoginPage");
        }

        public ICommand TakePictureCommand { get { return new RelayCommand(TakePicture); } }

        private async void TakePicture()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await dialogService.ShowMessage("No Camera", ":( No camera available.");
                return;
            }

            IsRunning = true;

            file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg",
                PhotoSize = PhotoSize.Small,
            });

            if (file != null)
            {
                ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            }

            IsRunning = false;
        }
        #endregion
    }
}
