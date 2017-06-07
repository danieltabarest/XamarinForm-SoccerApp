using SoccerApp.ViewModels;
using Xamarin.Forms;

namespace SoccerApp.Views
{
    public partial class MyResultsPage : ContentPage
    {
        public MyResultsPage()
        {
            try
            {
                InitializeComponent();

                //var selectMatchViewModel = SelectMatchViewModel.GetInstance();
                //base.Appearing += (object sender, System.EventArgs e) =>
                //{
                //    selectMatchViewModel.RefreshCommand.Execute(this);
                //};
            }
            catch (System.Exception ex)
            {

                throw;
            }
        
        }
    }
}
