using SoccerApp.ViewModels;
using SoccerApp.ViewModels;
using Xamarin.Forms;

namespace SoccerApp.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            try
            {
                InitializeComponent();
                BindingContext = new LoginViewModel();
            }
            catch (System.Exception ex)
            {

                throw;
            }

        }
    }
}
