using SoccerApp.ViewModels;
using Xamarin.Forms;

namespace SoccerApp.Views
{
    public partial class SelectMatchPage : ContentPage
    {
        public SelectMatchPage()
        {
            try
            {
                InitializeComponent();

                var selectMatchViewModel = SelectMatchViewModel.GetInstance();
                base.Appearing += (object sender, System.EventArgs e) =>
                {
                    selectMatchViewModel.RefreshCommand.Execute(this);
                };
            }
            catch (System.Exception ex)
            {

                throw;
            }
        
        }
    }
}
