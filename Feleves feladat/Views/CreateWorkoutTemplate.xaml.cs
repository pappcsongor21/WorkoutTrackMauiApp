using Feleves_feladat.ViewModels;

namespace Feleves_feladat.Views;

public partial class WorkoutCreatorPage : ContentPage
{
    CreateWorkoutTemplateViewModel vm;
    public WorkoutCreatorPage(CreateWorkoutTemplateViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        this.vm = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        vm.Initialize();
    }
}