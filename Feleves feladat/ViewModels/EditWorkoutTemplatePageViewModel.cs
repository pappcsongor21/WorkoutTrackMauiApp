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
    public partial class EditWorkoutTemplatePageViewModel(IDbService db) : ObservableObject
    {
        private readonly IDbService db = db;

        public int EditedWorkoutTemplateId
        {
            set
            {
                _ = LoadEditedWorkoutTemplateAsync(value);
            }
        }

        private async Task LoadEditedWorkoutTemplateAsync(int value)
        {
            EditedWorkoutTemplate = await db.GetWorkoutTemplateByIdAsync(value);
        }

        [ObservableProperty]
        private Workout editedWorkoutTemplate;

        public ObservableCollection<string> Colors { get; } =
        [
            "Red", "Green", "Blue", "Orange", "Purple", "Yellow"
        ];

        [RelayCommand]
        public async Task DeleteExerciseAsync(Exercise exercise)
        {
            await db.DeleteExerciseAsync(exercise);
            EditedWorkoutTemplate.Exercises.Remove(exercise);
        }

        [RelayCommand]
        public async Task SaveWorkoutAsync()
        {
            await db.UpdateWorkoutTemplateAsync(EditedWorkoutTemplate);
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        public async Task GoToSelectExerciseAsync()
        {
            var currentWorkoutParams = new ShellNavigationQueryParameters
            {
                { "workoutTemplateId", EditedWorkoutTemplate.Id }
            };
            await Shell.Current.GoToAsync("selectexercise", currentWorkoutParams);
        }
        public async Task InitializeExercisesAsync()
        {
            //if (EditedWorkoutTemplate == null) return;

            EditedWorkoutTemplate.Exercises = new ObservableCollection<Exercise>
                (await db.GetExercisesByWorkoutIdAsync(EditedWorkoutTemplate.Id));

        }

    }
}
