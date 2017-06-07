
using SoccerApp.ViewModels;
using Xamarin.Forms;

namespace SoccerApp.Views
{
    public partial class NewUserPage : ContentPage
    {
        public NewUserPage()
        {
            InitializeComponent();
            BindingContext = new NewUserViewModel();
        }
    }
}
