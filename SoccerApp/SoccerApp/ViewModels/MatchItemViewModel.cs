using GalaSoft.MvvmLight.Command;
using SoccerApp.Models;
using SoccerApp.Services;
using System.Windows.Input;

namespace SoccerApp.ViewModels
{

    namespace Soccer.ViewModels
    {
        public class MatchItemViewModel : Match
        {

            #region Attributes
            private ApiService apiService;
            private DataService dataService;
            private DialogService dialogService;
            private NavigationService navigationService;
            private bool isRefreshing = false;
            private int tournamentId;
            #endregion

            public MatchItemViewModel()
            {
                navigationService = new NavigationService();
            }

            public ICommand SelectMatchCommand { get { return new RelayCommand(SelectMatch); } }

            private async void SelectMatch()
            {
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.EditPreditions = new EditPreditionsViewModel(this);
                await navigationService.Navigate("EditPreditionsPage");
            }
        }
    }
}
