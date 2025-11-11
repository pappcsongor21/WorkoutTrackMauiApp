using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Feleves_feladat.Models;
using Feleves_feladat.Services;
using System.Collections.ObjectModel;

namespace Feleves_feladat.ViewModels
{
    public partial class WorkoutCreatorPageViewModel
        (WorkoutBuilderService workoutBuilderService, IDbService db)
        : ObservableObject
    {
        private readonly WorkoutBuilderService workoutBuilderService = workoutBuilderService;
        private readonly IDbService db = db;

        [ObservableProperty]
        private string name = "New workout";

        public ObservableCollection<Exercise> Exercises => workoutBuilderService.CurrentExercises;

        [RelayCommand]
        public async Task SaveWorkoutAsync()
        {
            Workout newWorkout = new() { Name = Name, Color = "Red", IsTemplate = true};
            await db.CreateWorkoutTemplateAsync(newWorkout);

            foreach (Exercise e in Exercises)
            {
                var newExercise = e.GetDeepCopy();
                newExercise.WorkoutId = newWorkout.Id;
                await db.CreateExerciseAsync(newExercise);
                newWorkout.Exercises.Add(newExercise);
            }

            workoutBuilderService.CurrentExercises.Clear();
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        public async Task GoToSelectExerciseAsync()
        {
            await Shell.Current.GoToAsync("selectexercise");
        }
    }
}
