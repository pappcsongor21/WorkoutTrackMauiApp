namespace Feleves_feladat;

public partial class WorkoutPage : ContentPage
{
	public WorkoutPage(WorkoutPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}