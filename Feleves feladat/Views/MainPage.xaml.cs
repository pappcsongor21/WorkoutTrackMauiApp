namespace Feleves_feladat
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Shell.SetNavBarIsVisible(this, false);
            //ezt azért, hogy ne legyen fent az a Home felirat
        }


        //private async void OnTrainButtonClicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new ChooseWorkoutPage());
        //}
        //Ehelyett csináltam commandot
    }
}
