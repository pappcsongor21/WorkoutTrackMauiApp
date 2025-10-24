namespace Feleves_feladat;

public partial class ChooseWorkoutPage : ContentPage
{
	ChooseWorkoutPageViewModel vm;
	public ChooseWorkoutPage(ChooseWorkoutPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		this.vm = vm;
    }

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await vm.InitializeAsync();
	}
}