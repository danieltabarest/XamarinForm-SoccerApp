using GalaSoft.MvvmLight.Command;
using SoccerApp.Models;
using SoccerApp.Services;
using System.Windows.Input;


namespace SoccerApp.ViewModels
{

    namespace Soccer.ViewModels
    {

        public class GroupItemViewModel : Group
        {

            private NavigationService navigationService;

            private DataService dataService;
            public GroupItemViewModel()
            {
                dataService = new DataService();
                navigationService = new NavigationService();
            }

            public ICommand SelectGroupCommand { get { return new RelayCommand(SelectGroup); } }

            private async void SelectGroup()
            {
                var mainViewModel = MainViewModel.GetInstance();

                var parameters = dataService.First<Parameter>(false);

                if (parameters.Option == "Tournaments")
                {
                    mainViewModel.Position = new PositionsViewModel(TournamentGroupId);
                    await navigationService.Navigate("PositionPage");
                }
                else
                {
                    mainViewModel.MyResult = new MyResultViewModel(TournamentGroupId);
                    await navigationService.Navigate("MyResultsPage");
                }
            }

        }

    }
}
