using Feleves_feladat.ViewModels;

namespace Feleves_feladat.Views;

public partial class EditWorkoutTemplatePage : ContentPage
{
	EditWorkoutTemplatePageViewModel vm;
	public EditWorkoutTemplatePage(EditWorkoutTemplatePageViewModel vm)
	{
		InitializeComponent();
		this.vm = vm;
		BindingContext = vm;
	}

	protected override async void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
		await vm.InitializeExercisesAsync();
    }
}