using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Feleves_feladat.Models;
using Feleves_feladat.Services;
using System.Collections.ObjectModel;

namespace Feleves_feladat.ViewModels
{
    public partial class SelectExercisePageViewModel
        (IDbService db, WorkoutBuilderService workoutBuilderService) : ObservableObject
    {
        private readonly IDbService db = db;
        private readonly WorkoutBuilderService workoutBuilderService = workoutBuilderService;

        public ObservableCollection<Exercise> Exercises { get; set; } = [];

        [RelayCommand]
        public async Task SelectExerciseAsync(Exercise selected)
        {
            workoutBuilderService.CurrentExercises.Add(selected);
            await Shell.Current.GoToAsync("..");
        }
        public async Task Init()
        {
            Exercises.Clear();
            var es = await db.GetExercisesAsync();
            foreach (var exercise in es)
            {
                Exercises.Add(exercise);
            }
        }
    }
}
