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
    [QueryProperty(nameof(EditedWorkoutTemplateId), "editedWorkoutTemplateId")]
    public partial class EditWorkoutTemplatePageViewModel : ObservableObject
    {
        private readonly IDbService db;
        private readonly WorkoutBuilderService workoutBuilderService;
        private readonly WorkoutNavigationState navState;

        public EditWorkoutTemplatePageViewModel(IDbService db,
            WorkoutBuilderService workoutBuilderService)
        {
            this.db = db;
            this.workoutBuilderService = workoutBuilderService;
        }

        [ObservableProperty]
        private Workout editedWorkoutTemplate;
        [ObservableProperty]
        private int editedWorkoutTemplateId;
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
                    newExercise.IsTemplate = false;
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
        public async Task InitializeAsync()
        {
            EditedWorkoutTemplate = await db.GetWorkoutTemplateByIdAsync(EditedWorkoutTemplateId);
            LoadExercisesFromDbAsync();
        }
        private async Task LoadExercisesFromDbAsync()
        {
            if (EditedWorkoutTemplate is null)
                return;

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
