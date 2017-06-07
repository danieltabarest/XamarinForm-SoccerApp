using GalaSoft.MvvmLight.Command;
using Plugin.Connectivity;
using SoccerApp.Models;
using SoccerApp.Services;
using System.ComponentModel;
using System.Windows.Input;

namespace SoccerApp.ViewModels
{

    namespace Soccer.ViewModels
    {
        public class EditPreditionsViewModel : Match, INotifyPropertyChanged
        {
            private Match match;

            #region Attributes
            private User currentUser;
            private ApiService apiService;
            private DataService dataService;
            private DialogService dialogService;
            private NavigationService navigationService;

            private bool isRunning;

            private bool isEnabled;
            private bool isRefreshing = false;

            public event PropertyChangedEventHandler PropertyChanged;
            #endregion
            public EditPreditionsViewModel(Match matchs)
            {
                this.match = matchs;
                apiService = new ApiService();
                dialogService = new DialogService();
                dataService = new DataService();
                navigationService = new NavigationService();

                DateId = matchs.DateId;
                DateTime = matchs.DateTime;
                LocalId = matchs.LocalId;
                LocalGoals = matchs.LocalGoals;
                Local = matchs.Local;
                MatchId = matchs.MatchId;
                StatusId = matchs.StatusId;
                TournamentGroupId = matchs.TournamentGroupId;
                VisitorId = matchs.VisitorId;
                VisitorGoals = matchs.VisitorGoals;
                Visitor = matchs.Visitor;
                WasPredicted = matchs.WasPredicted;

                GoalsLocal = LocalGoals2.ToString();
                GoalsVisitor = VisitorGoals2.ToString();
                isEnabled = true;
            }


            public ICommand SaveCommand { get { return new RelayCommand(Save); } }

            private async void Save()
            {
                if (string.IsNullOrEmpty(GoalsLocal))
                {
                    await dialogService.ShowMessage("Error", "You must enter a valid local goals.");
                }

                if (string.IsNullOrEmpty(GoalsVisitor))
                {
                    await dialogService.ShowMessage("Error", "You must enter a valid visitor goals.");
                }

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

                isRunning = true;
                isEnabled = false;

                var parameters = dataService.First<Parameter>(false);
                var user = dataService.First<User>(false);

                var prediction = new Prediction
                {
                    LocalGoals = int.Parse(GoalsLocal)
                    ,
                    MatchId = MatchId,
                    Points = 0,
                    UserId = user.UserId,
                    VisitorGoals = int.Parse(GoalsVisitor),

                };

                var response = await apiService.Post(parameters.URLBase, "/api", "/Predictions", user.TokenType, user.AccessToken, prediction);

                isRunning = false;
                isEnabled = true;

                if (!response.IsSuccess)
                {

                    await dialogService.ShowMessage("Error", "Problem ocurred retrieving user information, try latter.");
                    return;
                }

                await navigationService.Back();
            }


            #region Properties
            public string GoalsLocal { get; set; }

            public string GoalsVisitor { get; set; }

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

            #endregion
        }
    }
}
