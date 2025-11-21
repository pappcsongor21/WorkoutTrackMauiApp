using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Feleves_feladat.Models;
using Feleves_feladat.Services;
using System.Collections.ObjectModel;

namespace Feleves_feladat.ViewModels
{
    public partial class CreateWorkoutTemplateViewModel : ObservableObject
    {
        IDbService db;
        public CreateWorkoutTemplateViewModel(IDbService db)
        {
            this.db = db;
            NewWorkout ??= new Workout() { Name = "New workout", Color = "red", IsTemplate = true };
            _ = db.CreateWorkoutTemplateAsync(newWorkout);
        }

        [ObservableProperty]
        private Workout newWorkout;

        public ObservableCollection<string> Colors { get; } = new()
        {
            "Red", "Green", "Blue", "Orange", "Purple", "Yellow"
        };


        [RelayCommand]
        public async Task SaveWorkoutAsync()
        {
            await db.UpdateWorkoutTemplateAsync(NewWorkout);
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        public async Task GoToSelectExerciseAsync()
        {
            var param = new ShellNavigationQueryParameters
            {
                {"workoutTemplateId", NewWorkout.Id }
            };
            await Shell.Current.GoToAsync("selectexercise", param);
        }
        [RelayCommand]
        public async Task DeleteExercise(Exercise exercise)
        {
            await db.DeleteExerciseAsync(exercise);
            NewWorkout.Exercises.Remove(exercise);
        }
        public async void Initialize()
        {
            NewWorkout.Exercises = new ObservableCollection<Exercise>
                (await db.GetExercisesByWorkoutIdAsync(NewWorkout.Id));
        }
    }
}
