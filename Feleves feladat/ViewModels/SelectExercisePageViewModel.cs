using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Feleves_feladat.Models;
using Feleves_feladat.Services;
using System.Collections.ObjectModel;

namespace Feleves_feladat.ViewModels
{
    [QueryProperty(nameof(WorkoutTemplateId), "workoutTemplateId")]
    public partial class SelectExercisePageViewModel
        (IDbService db) : ObservableObject
    {
        private readonly IDbService db = db;
        public int WorkoutTemplateId { get; set; }
        public ObservableCollection<Exercise> Exercises { get; set; } = [];

        [RelayCommand]
        public async Task SelectExerciseAsync(Exercise selected)
        {
            var newExercise = selected.GetDeepCopy();
            newExercise.WorkoutId = WorkoutTemplateId;
            newExercise.IsTemplate = false;
            await db.CreateExerciseAsync(newExercise);
            await Shell.Current.GoToAsync("..");
        }
        public async Task Init()
        {
            Exercises.Clear();
            var es = await db.GetExerciseTemplatesAsync();
            foreach (var exercise in es)
            {
                Exercises.Add(exercise);
            }
        }
    }
}
