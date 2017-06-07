using GalaSoft.MvvmLight.Command;
using SoccerApp.Models;
using SoccerApp.Services;
using System.Windows.Input;


namespace SoccerApp.ViewModels
{

    namespace Soccer.ViewModels
    {

        public class GroupUserItemViewModel : GroupUser
        {

            private NavigationService navigationService;


            public GroupUserItemViewModel()
            {
                navigationService = new NavigationService();
            }

            public ICommand SelectGroupCommand { get { return new RelayCommand(SelectGroup); } }

            private async void SelectGroup()
            {
                //var mainViewModel = MainViewModel.GetInstance();
                //mainViewModel.UserGroup= new UserGroupViewModel(this);
                //await navigationService.Navigate("UserGroupPage");
            }

        }

    }
}
