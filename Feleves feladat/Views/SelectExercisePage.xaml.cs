using Feleves_feladat.ViewModels;

namespace Feleves_feladat.Views;

public partial class SelectExercisePage : ContentPage
{
	SelectExercisePageViewModel vm;
	public SelectExercisePage(SelectExercisePageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		this.vm = vm;
	}

	protected async override void OnAppearing()
	{
		base.OnAppearing();
		await vm.Init();
	}
}