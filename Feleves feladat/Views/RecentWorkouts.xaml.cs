using Feleves_feladat.ViewModels;

namespace Feleves_feladat.Views;

public partial class RecentWorkouts : ContentPage
{
	private readonly RecentWorkoutsViewModel vm;
	public RecentWorkouts(RecentWorkoutsViewModel vm)
	{
		InitializeComponent();
		this.vm = vm;
		BindingContext = vm;
	}

	protected async override void OnAppearing()
	{
		base.OnAppearing();
		await vm.LoadWorkoutsFromDbAsync();
	}
}