using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Feleves_feladat.Models;
using Feleves_feladat.Services;
using System.Collections.ObjectModel;

namespace Feleves_feladat.ViewModels
{
    public partial class WorkoutCreatorPageViewModel(WorkoutCreatorService workoutBuilderService) : ObservableObject
    {
        private readonly WorkoutCreatorService workoutBuilderService = workoutBuilderService;
        public ObservableCollection<Exercise> Exercises => workoutBuilderService.CurrentExercises;

        [RelayCommand]
        public async Task GoToSelectExerciseAsync()
        {
            await Shell.Current.GoToAsync("selectexercise");
        }
    }
}
