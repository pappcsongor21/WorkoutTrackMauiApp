using Feleves_feladat.ViewModels;

namespace Feleves_feladat.Views;

public partial class RecentWorkoutsPage : ContentPage
{
	private readonly RecentWorkoutsPageViewModel vm;
	public RecentWorkoutsPage(RecentWorkoutsPageViewModel vm)
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