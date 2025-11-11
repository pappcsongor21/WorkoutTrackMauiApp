namespace Feleves_feladat;

public partial class WorkoutPage : ContentPage
{
    WorkoutPageViewModel vm;
    public WorkoutPage(WorkoutPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        this.vm = vm;
    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await vm.InitializeExercisesFromDb();
    }


}