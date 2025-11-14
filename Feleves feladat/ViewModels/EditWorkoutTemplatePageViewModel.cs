using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Feleves_feladat.Models;
using Feleves_feladat.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Feleves_feladat.ViewModels
{
    [QueryProperty(nameof(EditedWorkoutTemplate), "editedWorkoutTemplate")]
    public partial class EditWorkoutTemplatePageViewModel(IDbService db,
        WorkoutBuilderService workoutBuilderService) : ObservableObject
    {
        private readonly IDbService db = db;
        private readonly WorkoutBuilderService workoutBuilderService = workoutBuilderService;
        
        [ObservableProperty]
        private Workout editedWorkoutTemplate;
        public ObservableCollection<string> Colors { get; } =
        [
            "Red", "Green", "Blue", "Orange", "Purple", "Yellow"
        ];
        public ObservableCollection<Exercise> Exercises => workoutBuilderService.CurrentExercises;

        [RelayCommand]
        public async Task DeleteExerciseAsync(Exercise exercise)
        {
            await db.DeleteExerciseAsync(exercise);
            workoutBuilderService.CurrentExercises.Remove(exercise);
        }

        [RelayCommand]
        public async Task SaveWorkoutAsync()
        {
            foreach (Exercise e in Exercises)
            {
                if (!editedWorkoutTemplate.Exercises.Contains(e))
                {
                    var newExercise = e.GetDeepCopy();
                    newExercise.WorkoutId = editedWorkoutTemplate.Id;
                    await db.CreateExerciseAsync(newExercise);
                }
            }
            workoutBuilderService.CurrentExercises.Clear();
            workoutBuilderService.IsFirstOpenForEdit = true;
            await db.UpdateWorkoutTemplateAsync(editedWorkoutTemplate);
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        public async Task GoToSelectExerciseAsync()
        {
            await Shell.Current.GoToAsync("selectexercise");
        }
        public async Task LoadExercisesFromDbAsync()
        {
            var exercises = await db.GetExercisesByWorkoutIdAsync(EditedWorkoutTemplate.Id);
            foreach (var exercise in exercises)
            {
                EditedWorkoutTemplate.Exercises.Add(exercise);
                if(workoutBuilderService.IsFirstOpenForEdit)
                    workoutBuilderService.CurrentExercises.Add(exercise);
            }
            workoutBuilderService.IsFirstOpenForEdit = false;
        }
    }
}
