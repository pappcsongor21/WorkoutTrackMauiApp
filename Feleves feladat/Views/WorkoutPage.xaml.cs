namespace Feleves_feladat;

public partial class WorkoutPage : ContentPage
{
	public WorkoutPage(Workout workout)
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = workout;
	}
}