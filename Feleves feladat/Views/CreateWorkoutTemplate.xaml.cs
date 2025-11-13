using Feleves_feladat.ViewModels;

namespace Feleves_feladat.Views;

public partial class WorkoutCreatorPage : ContentPage
{
    public WorkoutCreatorPage(CreateWorkoutTemplateViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}