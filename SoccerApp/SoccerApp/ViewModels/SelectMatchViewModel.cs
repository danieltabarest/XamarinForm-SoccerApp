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
    public class SelectMatchViewModel : INotifyPropertyChanged
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
        public ObservableCollection<MatchItemViewModel> Matches { get; set; }

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
        public SelectMatchViewModel(int tournamentId)
        {
            try
            {
                this.tournamentId = tournamentId;
                instance = this;

                apiService = new ApiService();
                dialogService = new DialogService();
                navigationService = new NavigationService();
                dataService = new DataService();
                Matches = new ObservableCollection<MatchItemViewModel>();
                LoadMatches();
            }
            catch (System.Exception ex)
            {

                throw;
            }
            
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
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    await dialogService.ShowMessage("Error", "Check you internet connection.");
                    await navigationService.Clear();
                    return;
                }

                var parameter = dataService.First<Parameter>(false);
                var user = dataService.First<User>(false);
                var controller = string.Format("/Tournaments/GetMatchesToPredict/{0}/{1}", tournamentId, user.UserId);
                var response = await apiService.Get<Match>(parameter.URLBase, "/api", controller, user.TokenType, user.AccessToken);
                if (!response.IsSuccess)
                {
                    await dialogService.ShowMessage("Error", response.Message);
                    //await navigationService.Clear();
                    return;
                }
                
                    ReloadMatches((List<Match>)response.Result);
            }
            catch (System.Exception ex)
            {

                //throw;
            }
            
        }

        private void ReloadMatches(List<Match> matches)
        {
            Matches.Clear();
            foreach (var matchs in matches)
            {
                Matches.Add(new MatchItemViewModel
                {
                    DateId = matchs.DateId,
                    DateTime = matchs.DateTime,
                    LocalId = matchs.LocalId,
                    LocalGoals = matchs.LocalGoals,
                    Local = matchs.Local,
                    MatchId = matchs.MatchId,
                    StatusId = matchs.StatusId,
                    TournamentGroupId = matchs.TournamentGroupId,
                    VisitorId = matchs.VisitorId,
                    VisitorGoals = matchs.VisitorGoals,
                    Visitor= matchs.Visitor,
                    WasPredicted = matchs.WasPredicted
                });
            }
        }
        #endregion

        #region Commands
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
