using GalaSoft.MvvmLight.Command;
using SoccerApp.Models;
using SoccerApp.Services;
using System.Windows.Input;


namespace SoccerApp.ViewModels
{
    public class TournamentItemViewModel : Tournament
    {
        private NavigationService navigationService;
        private DataService dataService;

        public TournamentItemViewModel()
        {
            navigationService = new NavigationService();
            dataService = new DataService();
        }

        public ICommand SelectTournamentCommand { get { return new RelayCommand(SelectTournament); } }

        private async void SelectTournament()
        {
            var mainViewModel = MainViewModel.GetInstance();
            var parameters = dataService.First<Parameter>(false);

            if (parameters.Option== "Predictions")
            {
                mainViewModel.SelectMatch = new SelectMatchViewModel(TournamentId);
                await navigationService.Navigate("SelectMatchPage");
            }
            else
            {
                mainViewModel.SelectGroup = new SelectGroupViewModel(Groups);
                await navigationService.Navigate("SelectGroupPage");
            }

        }
    }


}
