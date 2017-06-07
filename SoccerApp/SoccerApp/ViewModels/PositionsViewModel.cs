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
    public class PositionsViewModel : INotifyPropertyChanged
    {
        #region Attributes
        private ApiService apiService;
        private DataService dataService;
        private DialogService dialogService;
        private NavigationService navigationService;
        private bool isRefreshing = false;
        private int tournamentGroupId;
        #endregion

        #region Properties
        public ObservableCollection<TournamentTeamItemViewModel> TournamentTeams { get; set; }

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
        public PositionsViewModel(int tournamentGroupId)
        {
            this.tournamentGroupId = tournamentGroupId;
            //instance = this;

            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            dataService = new DataService();

            TournamentTeams = new ObservableCollection<TournamentTeamItemViewModel>();
            LoadTournamentTeams();
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
        private async void LoadTournamentTeams()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await dialogService.ShowMessage("Error", "Check you internet connection.");
                await navigationService.Clear();
                return;
            }

            IsRefreshing = true;
            var parameter = dataService.First<Parameter>(false);
            var user = dataService.First<User>(false);
            var response = await apiService.Get<TournamentTeam>(parameter.URLBase, "/api", "/TournamentTeams", user.TokenType, user.AccessToken, tournamentGroupId);

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }
            ReloadTournaments((List<TournamentTeam>)response.Result);
            IsRefreshing = false;

        }

        private void ReloadTournaments(List<TournamentTeam> tournamentsteams)
        {
            TournamentTeams.Clear();
            foreach (var tournament in tournamentsteams)
            {
                TournamentTeams.Add(new TournamentTeamItemViewModel
                {

                    AgainstGoals = tournament.AgainstGoals,
                    FavorGoals = tournament.FavorGoals,
                    MatchesLost = tournament.MatchesLost,
                    MatchesPlayed = tournament.MatchesPlayed,
                    MatchesTied = tournament.MatchesTied,
                    MatchesWon = tournament.MatchesWon,
                    Points = tournament.Points,
                    Position = tournament.Position,
                    Team = tournament.Team,
                    TeamId = tournament.TeamId,
                    TournamentGroup = tournament.TournamentGroup,
                    TournamentGroupId = tournament.TournamentGroupId,
                    TournamentTeamId = tournament.TournamentTeamId,

                });
            }
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand { get { return new RelayCommand(Refresh); } }

        public void Refresh()
        {
            IsRefreshing = true;
            LoadTournamentTeams();
            IsRefreshing = false;
        }
        #endregion
    }


}
