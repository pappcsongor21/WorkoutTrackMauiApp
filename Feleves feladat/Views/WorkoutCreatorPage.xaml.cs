using Feleves_feladat.ViewModels;

namespace Feleves_feladat.Views;

public partial class WorkoutCreatorPage : ContentPage
{
	public WorkoutCreatorPage(WorkoutPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}